using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using System.Net;
using MiniBillingServer.IO;

namespace MiniBillingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Be_Config _Config = Config.cfg;
            Console.Title = "MiniBillingServer - by florian0";

            Http.HttpServer server = new Http.HttpServer();

            server.Prefixes.Add("http://127.0.0.1:" + _Config.Listen_Port + "/");

            server.Handlers.Add(new Handlers.ServerStateHandler());
            server.Handlers.Add(new Handlers.SilkDataCallHandler());

            Model.SilkDB.Instance.Init();

            server.Start();
            
            Console.Read();
        }
    }
}
