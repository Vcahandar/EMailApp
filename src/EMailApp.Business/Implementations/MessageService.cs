using EMailApp.Business.Interfaces;
using EMailApp.Core.Concrete;
using EMailApp.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailApp.Business.Implementations
{
    public class MessageService : IMessageService
    {
        readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public List<Message> GetListDeleteMessage()
        {
            return _messageRepository.GetByFilter(e => e.Status == false);
        }

        public List<Message> GetListReceiverMessage(string e)
        {
            return _messageRepository.GetByFilter(c => c.ReceiverMail == e);
        }

        public List<Message> GetListSenderMessage(string e)
        {
            return _messageRepository.GetByFilter(c => c.SenderMail == e);
        }

        public void TDelete(int id)
        {
            _messageRepository.Delete(id);
        }

        public Message TGetById(int id)
        {
            return _messageRepository.GetById(id);
        }

        public List<Message> TGetListAll()
        {
            return _messageRepository.GetList();
        }

        public void TInsert(Message t)
        {
            _messageRepository.Insert(t);
        }

        public void TUpdate(Message t)
        {
            _messageRepository.Update(t);
        }
    }
}
