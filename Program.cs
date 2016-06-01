using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using System.Net;

namespace MiniBillingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MiniBillingServer - by florian0";

            Http.HttpServer server = new Http.HttpServer();

            server.Prefixes.Add("http://localhost:8080/");

            server.Handlers.Add(new Handlers.ServerStateHandler());
            server.Handlers.Add(new Handlers.SilkDataCallHandler());

            Model.SilkDB.Instance.Init();

            server.Start();
            
            Console.Read();
        }
    }
}
