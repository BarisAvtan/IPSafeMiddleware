using Microsoft.Extensions.Options;
using System.Net;

namespace WebApplication1.Middleware
{
    public class IPSafeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IPList _ipList;
        
        //IOptions ile class içerisindeki okunur
        public IPSafeMiddleware(RequestDelegate next,IOptions<IPList> ipList)
        {
            this._next = next;
            this._ipList = ipList.Value;
            
        }
        public async Task Invoke (HttpContext context)
        {
            var reqIpAdress = context.Connection.RemoteIpAddress;
            var isWhiteList = _ipList.WhiteList.Where(x => IPAddress.Parse(x).Equals(reqIpAdress)).Any();
            if (!isWhiteList)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
            await _next(context);
        }
    }
   
}
