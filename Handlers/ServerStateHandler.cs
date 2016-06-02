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
            #region SecurityCheck
            string clientIP = context.Request.RemoteEndPoint.ToString();
            try { clientIP = clientIP.Substring(0, clientIP.IndexOf(":")); } catch { }

            List<string> HostIP = new List<string>();
            foreach (string AuthorizedHost in IO.Config.cfg.Allowed_Hosts)
            {
                HostIP.Add(Dns.GetHostAddresses(AuthorizedHost)[0].ToString());
            }

            if (IO.Config.cfg.Allowed_IPs.Contains(clientIP) || HostIP.Contains(clientIP)) { } else { return false; }

            #endregion
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
