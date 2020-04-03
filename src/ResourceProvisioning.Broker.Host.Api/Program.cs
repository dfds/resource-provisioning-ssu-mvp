﻿using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ResourceProvisioning.Broker.Host.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args)
				.Build()
				.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
