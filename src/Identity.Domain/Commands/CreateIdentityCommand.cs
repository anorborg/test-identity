using System.Security;

namespace Identity.Domain.Commands
{
    public sealed class CreatePasswordIdentityCommand : CreateIdentityCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public CreatePasswordIdentityCommand()
        {
        }

        public CreatePasswordIdentityCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    public abstract class CreateIdentityCommand
    {
    }
}