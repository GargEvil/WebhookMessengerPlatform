using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebhookMessengerPlatform.WebApi.Models
{
    public class MessengerRequestMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        public DateTime MessageDate { get; set; }

    }
}
