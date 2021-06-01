using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebhookMessengerPlatform.WebApi.Services;

namespace WebhookMessengerPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessengerWebhookController : ControllerBase
    {
        private readonly string verifyToken;
        private readonly IWebhookService _service;

        public MessengerWebhookController(IConfiguration config, IWebhookService service)
        {
            verifyToken = config.GetValue<string>("Security:verifyToken");
            _service = service;

        }

        [HttpGet]
        public string Webhook(
            [FromQuery(Name = "hub.mode")] string hub_mode,
            [FromQuery(Name = "hub.challenge")] string hub_challenge,
            [FromQuery(Name = "hub.verify_token")] string hub_verify_token)
        {

            if (hub_verify_token == verifyToken)
            {
                return hub_challenge;
            }
            else
            {
                return "";
            }

        }

        [HttpPost]
        public async Task Webhook()
        {
            string json;
            try
            {
                using (StreamReader sr = new StreamReader(this.Request.Body))
                {
                    json = sr.ReadToEnd();
                }

                var data = JsonConvert.DeserializeObject(json);
                var signatureHeader = Request.Headers["X-Hub-Signature"].ToString();

                if(signatureHeader != null)
                {
                    var signature = _service.CalculateSignature(json);
                    if(signature == signatureHeader)
                    {
                        
                    }
                }



            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
