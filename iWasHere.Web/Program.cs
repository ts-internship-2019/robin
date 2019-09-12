using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
//test push github Alexandru Vasile

namespace iWasHere.Web
{
    public class Program
    {
        //1209 Mihai Popa test
        //...
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        //vgmaxim 3.33
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
