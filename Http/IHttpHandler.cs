using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniBillingServer.Http
{
    interface IHttpHandler
    {
        bool Handle(System.Net.HttpListenerContext context);
    }
}
