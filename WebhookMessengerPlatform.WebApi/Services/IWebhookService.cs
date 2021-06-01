using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebhookMessengerPlatform.WebApi.Services
{
    public interface IWebhookService
    {
         string CalculateSignature(string payload);
         Task StoreData(dynamic data)
    }
}
