using EMailApp.Core.Concrete;
using EMailApp.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailApp.DataAccess.Repositories.Implementations
{
    public class MessageRepository :Repository<Message>,IMessageRepository
    {
    }
}
