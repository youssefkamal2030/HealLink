using ErrorOr;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Services
{
    public class ChatService(IChatRepository chatRepository, IUserRoleResolver userRoleResolver, IDoctorPatientDoctorPatientConnectionRepository DoctorPatientConnectionRepository) : IChatService
    {
        private readonly IChatRepository _chatRepository = chatRepository;
        private readonly IUserRoleResolver _userRoleResolver = userRoleResolver;
        private readonly IDoctorPatientDoctorPatientConnectionRepository _DoctorPatientConnectionRepository = DoctorPatientConnectionRepository;

        public async Task<Result<List<ChatMessage>>> GetChatHistoryAsync(Guid userId1, Guid userId2)
        {
            var chatHistory = await _chatRepository.GetChatHistoryAsync(userId1, userId2);
            if (chatHistory == null)
            {
                return Result<List<ChatMessage>>.Failure("No chat history found between the specified users.");

            }
                return Result<List<ChatMessage>>.Success(chatHistory);
            
        }

    public async Task<Result<Guid>> SendMessageAsync(Guid senderId, Guid receiverId, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return Result<Guid>.Failure("Message content cannot be empty.");

        if (senderId == Guid.Empty)
            return Result<Guid>.Failure("SenderId is required.");

        if (receiverId == Guid.Empty)
            return Result<Guid>.Failure("ReceiverId is required.");

        var chatMessage = new ChatMessage(senderId, receiverId, message);

        await _chatRepository.AddChatMessageAsync(chatMessage);

        return Result<Guid>.Success(chatMessage.Id);
    }

        public async Task<Result<bool>> ValidateConnection(Guid senderId, Guid receiverId)
        {
            var senderInfo = await _userRoleResolver.ResolveUserAsync(senderId);
            var receiverInfo = await _userRoleResolver.ResolveUserAsync(receiverId);

            if (senderInfo == null)
            {
                return Result<bool>.Failure("Sender user not found or invalid role.");
            }

            if (receiverInfo == null)
            {
                return Result<bool>.Failure("Receiver user not found or invalid role.");
            }

            var (senderRole, senderEntityId) = senderInfo.Value;
            var (receiverRole, receiverEntityId) = receiverInfo.Value;

            bool areConnected = false;

            if (senderRole == UserRole.Doctor && receiverRole == UserRole.Patient)
            {
                areConnected = await _DoctorPatientConnectionRepository.ConnectionExistsAsync(senderEntityId, receiverEntityId);
            }
            else if (senderRole == UserRole.Patient && receiverRole == UserRole.Doctor)
            {
                areConnected = await _DoctorPatientConnectionRepository.ConnectionExistsAsync(receiverEntityId, senderEntityId);
            }
            else
            {
                return Result<bool>.Failure("Chat is only allowed between doctors and patients.");
            }

            if (!areConnected)
            {
                return Result<bool>.Failure("No connection exists between the doctor and patient.");
            }

            return Result<bool>.Success(true);
        }
    }
}
