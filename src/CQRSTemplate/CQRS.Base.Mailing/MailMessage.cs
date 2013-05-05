using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CQRS.Base.Mailing
{
    public class MailMessage<T> : IMailMessage<T> where T : class
    {
        public MailMessage(){}

        public MailMessage(T model)
        {
            MessageParameters = model;
        } 

        public string TemplateName { get; set; }

        public ICollection<string> Recipients { get; set; }

        public string Sender { get; set; }

        public T MessageParameters { get; set; }
    }
}
