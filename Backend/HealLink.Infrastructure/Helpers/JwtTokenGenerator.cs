using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HealLink.Domain.Entities;
using healLink.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private static readonly Byte[] _privateKey = new Byte[] { 0xDE, 0xAD, 0xBE, 0xEF }; // NOTE: You should use a private-key that's a LOT longer than just 4 bytes.
    private static readonly TimeSpan _passwordResetExpiry = TimeSpan.FromMinutes(5);
    private const Byte _version = 1; // Increment this whenever the structure of the message changes.
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> GenerateTokenAsync(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
            signingCredentials: creds
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public Task<string> GenerateRefreshTonkenAsync(User user)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }

    public Task<bool> ValidateTokenAsync(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = key
            }, out SecurityToken validatedToken);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public string CreatePasswordResetHmacCode(Guid userId)
    {
        Byte[] message = Enumerable.Empty<Byte>()
            .Append(_version)
            .Concat(userId.ToByteArray())
            .Concat(BitConverter.GetBytes(DateTime.UtcNow.ToBinary()))
            .ToArray();

        using (HMACSHA256 hmacSha256 = new HMACSHA256(key: _privateKey))
        {
            Byte[] hash = hmacSha256.ComputeHash(buffer: message, offset: 0, count: message.Length);
            Byte[] outputMessage = message.Concat(hash).ToArray();
            String outputCodeB64 = Convert.ToBase64String(outputMessage);
            String outputCode = outputCodeB64.Replace('+', '-').Replace('/', '_');
            return outputCode;
        }
    }

    public bool VerifyPasswordResetHmacCode(String codeBase64Url, out Guid userId)
    {
        userId = Guid.Empty;

        try
        {
            String base64 = codeBase64Url.Replace('-', '+').Replace('_', '/');

            while (base64.Length % 4 != 0)
            {
                base64 += "=";
            }

            Byte[] message = Convert.FromBase64String(base64);

            
            const Int32 minimumMessageLength = 1 + 16 + sizeof(Int64) + 32; 
            if (message.Length < minimumMessageLength)
                return false;

            Byte version = message[0];
            if (version < _version)
                return false;

          
            Byte[] userIdBytes = new Byte[16];
            Array.Copy(message, 1, userIdBytes, 0, 16);
            userId = new Guid(userIdBytes);

            
            Int64 createdUtcBinary = BitConverter.ToInt64(message, startIndex: 17);
            DateTime createdUtc = DateTime.FromBinary(createdUtcBinary);

            if (createdUtc.Add(_passwordResetExpiry) < DateTime.UtcNow)
                return false;

            const Int32 messageLength = 1 + 16 + sizeof(Int64); 

            using (HMACSHA256 hmacSha256 = new HMACSHA256(key: _privateKey))
            {
                Byte[] hash = hmacSha256.ComputeHash(message, offset: 0, count: messageLength);
                Byte[] messageHash = message.Skip(messageLength).ToArray();
                return Enumerable.SequenceEqual(hash, messageHash);
            }
        }
        catch
        {
            userId = Guid.Empty;
            return false;
        }
    }
}
 