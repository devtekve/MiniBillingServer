using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

namespace MiniBillingServer.Handlers
{
    class ServerStateHandler : Http.IHttpHandler
    {

        public bool Handle(HttpListenerContext context)
        {
            // Validate Handler
            if (context.Request.Url.LocalPath.ToLower() != "/billing_serverstate.asp")
            {
                return false;
            }

            // Build response
            HttpListenerResponse response = context.Response;

            string responseString = "1";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            
            response.ContentLength64 = buffer.Length;

            System.IO.Stream output = response.OutputStream;
            
            output.Write(buffer, 0, buffer.Length);
            
            output.Close();

            return true;
        }
    }
}
