using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebhookMessengerPlatform.WebApi.Models;

namespace WebhookMessengerPlatform.WebApi.Services
{
    public class WebhookService : IWebhookService
    {
        private readonly string appSecret;
        private readonly string appToken;
        private readonly HttpClient _httpClient = new HttpClient();

        public WebhookService(IConfiguration config)
        {
            appSecret = config.GetValue<string>("Security:appSecret");
            appToken = config.GetValue<string>("Security:appToken");
        }

        public async Task StoreData(dynamic data)
        {
            try
            {
                var model = new MessengerRequestMessage()
                {
                    Id = data.entry[0].id,
                    SenderId = data.entry[0].messaging[0].sender.id,
                    Message = data.entry[0].messaging[0].message.text
                };

                model.MessageDate = DateTimeOffset.FromUnixTimeMilliseconds(data.entry[0].time);

                if(model != null)
                {
                    await SendMessages(model);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task SendMessages(MessengerRequestMessage model)
        {
            var response = new MessengerResponseModel()
            {
                recipient = new MessengerResponseModel.Recipient()
                {
                    id = model.SenderId
                },
                message = new MessengerResponseModel.Message()
                {
                    text = model.Message
                },
                messaging_type = "RESPONSE"
            };

            await _httpClient.PostAsJsonAsync($"https://graph.facebook.com/v10.0/me/messages?access_token=" + appToken, response );
        }

        public string CalculateSignature(string payload)
        {
            /*
             Please note that the calculation is made on the escaped unicode version of the payload, with lower case hex digits.
             If you just calculate against the decoded bytes, you will end up with a different signature.
             For example, the string äöå should be escaped to \u00e4\u00f6\u00e5.
             */
            payload = EncodeNonAsciiCharacters(payload);

            byte[] secretKey = Encoding.UTF8.GetBytes(appSecret);
            HMACSHA1 hmac = new HMACSHA1(secretKey);
            hmac.Initialize();
            byte[] bytes = Encoding.UTF8.GetBytes(payload);
            byte[] rawHmac = hmac.ComputeHash(bytes);

            return ByteArrayToString(rawHmac).ToLower();
        }

        private static string EncodeNonAsciiCharacters(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127)
                {
                    string encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
    }
}
