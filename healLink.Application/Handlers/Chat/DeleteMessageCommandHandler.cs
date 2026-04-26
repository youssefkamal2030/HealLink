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
    //   2. Call message.SoftDelete() without requestingUserId parameter
    //   3. Handler will only handle business logic exceptions
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, Result<bool>>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMessageCommandHandler(
            IChatRepository chatRepository,
            IUnitOfWork unitOfWork)
        {
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            // Get the message
            var message = await _chatRepository.GetByIdAsync(request.MessageId, cancellationToken);
            if (message == null)
                return Result<bool>.Failure("Message not found.");

            // Check if already deleted
            if (message.IsDeleted)
                return Result<bool>.Failure("Message is already deleted.");

            try
            {
                // Soft delete the message (domain method handles authorization)
                message.SoftDelete(request.RequestingUserId);

                await _chatRepository.UpdateAsync(message, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<bool>.Success(true);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to delete message: {ex.Message}");
            }
        }
    }
}
