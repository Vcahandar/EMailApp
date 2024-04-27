using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailApp.Core.Concrete
{
    public class Draft
    {
        public int DraftId { get; set; }
        public string SenderName { get; set; }
        public string SenderMail { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverMail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }

    }
}
