using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;

namespace HealLink.Infrastructure.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRoleResolver _userRoleResolver;
        private readonly IDoctorPatientConnectionRepository _connectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorRepository _doctorRepository;

        public ChatService(
            IChatRepository chatRepository,
            IUserRoleResolver userRoleResolver,
            IDoctorPatientConnectionRepository connectionRepository,
            IUnitOfWork unitOfWork,
            IDoctorRepository doctorRepository)
        {
            _chatRepository = chatRepository;
            _userRoleResolver = userRoleResolver;
            _connectionRepository = connectionRepository;
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
        }

        public async Task<Result<List<ChatMessage>>> GetChatHistoryAsync(Guid userId1, Guid userId2)
        {
            var chatHistory = await _chatRepository.GetChatHistoryAsync(userId1, userId2);
            return chatHistory == null
                ? Result<List<ChatMessage>>.Failure("No chat history found between the specified users.")
                : Result<List<ChatMessage>>.Success(chatHistory);
        }

        public async Task<Result<Guid>> SendMessageAsync(Guid senderId, Guid receiverId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return Result<Guid>.Failure("Message content cannot be empty.");

            if (senderId == Guid.Empty)
                return Result<Guid>.Failure("SenderId is required.");

            if (receiverId == Guid.Empty)
                return Result<Guid>.Failure("ReceiverId is required.");

            var chatMessage = ChatMessage.Send(senderId, receiverId, message);

            await _chatRepository.AddChatMessageAsync(chatMessage);
            await _unitOfWork.SaveChangesAsync(); 

            return Result<Guid>.Success(chatMessage.Id);
        }

        public async Task<Result<bool>> ValidateConnection(Guid senderId, Guid receiverId)
        {
            var senderInfo = await _userRoleResolver.ResolveUserAsync(senderId);
            var receiverInfo = await _userRoleResolver.ResolveUserAsync(receiverId);

            if (senderInfo == null)
                return Result<bool>.Failure("Sender user not found or invalid role.");

            if (receiverInfo == null)
                return Result<bool>.Failure("Receiver user not found or invalid role.");

            var (senderRole, senderEntityId) = senderInfo.Value;
            var (receiverRole, receiverEntityId) = receiverInfo.Value;

            Guid doctorEntityId;
            Guid patientEntityId;

            if (senderRole == UserRole.Doctor && receiverRole == UserRole.Patient)
            {
                doctorEntityId = senderEntityId;
                patientEntityId = receiverEntityId;
            }
            else if (senderRole == UserRole.Patient && receiverRole == UserRole.Doctor)
            {
                doctorEntityId = receiverEntityId;
                patientEntityId = senderEntityId;
            }
            else
            {
                return Result<bool>.Failure("Chat is only allowed between doctors and patients.");
            }

            var isAccepted = await _connectionRepository.AcceptedConnectionExistsAsync(doctorEntityId, patientEntityId);
            if (!isAccepted)
                return Result<bool>.Failure("No accepted connection exists between the doctor and patient.");

            // BR-CHAT-02: doctor must have chat availability enabled
            var doctor = await _doctorRepository.GetByDoctorId(doctorEntityId);
            if (doctor != null && !doctor.IsAvailableForChat)
                return Result<bool>.Failure("The doctor is not currently available for chat.");

            return Result<bool>.Success(true);
        }
    }
}
