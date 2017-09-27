using SocialNetwork2017.Domain;
using System.Collections.Generic;

namespace SocialNetwork2017.BL.Interfaces
{
    public interface IMessagesRepository
    {
        #region Входящие сообщения

        IEnumerable<IncomingMessage> GetIncomingMessages();
        IEnumerable<IncomingMessage> GetIncomingMessagesByUserId(int userId);
        void SaveIncomingMessage(IncomingMessage message);
        void DeleteIncomingMessage(IncomingMessage message);

        #endregion

        #region Исходящие сообщения

        IEnumerable<OutgoingMessage> GetOutgoingMessages();
        IEnumerable<OutgoingMessage> GetOutgoingMessagesByUserId(int userId);
        void SaveOutgoingMessage(OutgoingMessage message);
        void DeleteOutgoingMessage(OutgoingMessage message);

        #endregion
    }
}
