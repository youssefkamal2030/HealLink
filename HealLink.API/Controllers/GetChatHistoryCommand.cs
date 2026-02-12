
namespace HealLink.Api.Controllers
{
    internal class GetChatHistoryCommand
    {
        private Guid userId1;
        private Guid userId2;

        public GetChatHistoryCommand(Guid userId1, Guid userId2)
        {
            this.userId1 = userId1;
            this.userId2 = userId2;
        }
    }
}