using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebhookMessengerPlatform.WebApi.Models
{
    // https://json2csharp.com/ -> I used this page
    public class MessengerResponseModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Recipient
        {
            public string id { get; set; }
        }

        public class Message
        {
            public string text { get; set; }
        }

            public string messaging_type { get; set; }
            public Recipient recipient { get; set; }
            public Message message { get; set; }
        


    }
}
