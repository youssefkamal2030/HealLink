using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Entities;

public class OTP
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime ExpiryTime { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }
}
