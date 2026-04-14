namespace HealLink.Infrastructure.Settings;

// Maps to the "EmailSettings" section in appsettings.json
// Used to construct the IEmailSender (ASP.NET Identity UI) in Program.cs
public class MailSettings
{
    public string Email { get; set; } = string.Empty;       // maps to SenderEmail
    public string AppPassword { get; set; } = string.Empty; // maps to Password
    public string Host { get; set; } = string.Empty;        // maps to SmtpServer
    public int Port { get; set; }
    public bool SSL { get; set; }
    public bool IsBodyHtml { get; set; }
}