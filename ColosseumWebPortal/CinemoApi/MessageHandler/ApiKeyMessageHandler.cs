using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CinemoApi.Models;

namespace CinemoApi.MessageHandler
{
    public class ApiKeyMessageHandler : DelegatingHandler
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool validKey = false;
            var checkApiExists = request.Headers.TryGetValues("ApiKey", out var requestHeaders);
            var apiValue = requestHeaders.ToList();
            string ad = apiValue[0];
            var apiAvailability = db.ApiKeys.Where(p => p.KeyString == ad).ToList();
            if (checkApiExists)
            {
                if (apiAvailability.Count > 0)
                {
                    validKey = true;
                }
            }
            if (!validKey) { return request.CreateResponse(HttpStatusCode.Forbidden, "Api Key Not Valid"); }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}