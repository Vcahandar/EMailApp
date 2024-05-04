using EMailApp.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailApp.Business.Interfaces
{
    public interface IMessageService : IGenericService<Message>
    {
        List<Message> GetListSenderMessage(string e);
        List<Message> GetListReceiverMessage(string e,string searchText);

        List<Message> GetListDeleteMessage();
    }
}
