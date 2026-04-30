using healLink.Application.Commands.Chat;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Handlers.Chat
{
    // TODO: [REFACTOR-AUTH] Remove authorization exception handling after centralized-authorization-infrastructure is implemented
    // PROBLEM: Handler catches UnauthorizedAccessException from domain entity
    // FIX: Remove try-catch block for UnauthorizedAccessException
    // APPROACH: Authorization will be handled by AuthorizationBehavior before handler executes
    // REASON: Authorization failures will be caught by pipeline, not handler
    // MIGRATION STEPS:
    //   1. Remove try-catch block entirely
    //   2. Call message.EditContent(request.NewContent) without requestingUserId parameter
    //   3. Handler will only handle business logic exceptions (ArgumentException)
    public class EditMessageCommandHandler : IRequestHandler<EditMessageCommand, Result<bool>>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditMessageCommandHandler(
            IChatRepository chatRepository,
            IUnitOfWork unitOfWork)
        {
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(EditMessageCommand request, CancellationToken cancellationToken)
        {
            // Get the message
            var message = await _chatRepository.GetByIdAsync(request.MessageId, cancellationToken);
            if (message == null)
                return Result<bool>.Failure("Message not found.");

            // Check if message is deleted
            if (message.IsDeleted)
                return Result<bool>.Failure("Cannot edit a deleted message.");

          
                message.EditContent(request.NewContent);

                await _chatRepository.UpdateAsync(message, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result<bool>.Success(true);
            
          
        }
    }
}
