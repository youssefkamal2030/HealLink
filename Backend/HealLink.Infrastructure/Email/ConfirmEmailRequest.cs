

namespace HealLink.Infrastructure.Email;

public record ConfirmEmailRequest(
 string Email,
 string Code
);
