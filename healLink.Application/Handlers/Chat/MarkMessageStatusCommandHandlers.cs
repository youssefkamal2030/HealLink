using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Chat;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Handlers.Chat
{
    public class MarkAsDeliveredCommandHandler : IRequestHandler<MarkAsDeliveredCommand, Result<bool>>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkAsDeliveredCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork)
        {
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(MarkAsDeliveredCommand request, CancellationToken cancellationToken)
        {
            var message = await _chatRepository.GetByIdAsync(request.MessageId, cancellationToken);
            if (message == null)
                return Result<bool>.Failure("Message not found.");

            try
            {
                message.MarkAsDelivered();
            }
            catch (System.InvalidOperationException ex)
            {
                return Result<bool>.Failure(ex.Message);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }

    public class MarkAsReadCommandHandler : IRequestHandler<MarkAsReadCommand, Result<bool>>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkAsReadCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork)
        {
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
        {
            var message = await _chatRepository.GetByIdAsync(request.MessageId, cancellationToken);
            if (message == null)
                return Result<bool>.Failure("Message not found.");

            try
            {
                message.MarkAsRead();
            }
            catch (System.InvalidOperationException ex)
            {
                return Result<bool>.Failure(ex.Message);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
