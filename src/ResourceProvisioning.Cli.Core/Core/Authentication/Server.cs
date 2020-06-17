using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

namespace ResourceProvisioning.Cli.Core.Core.Authentication
{
	public class Server
	{
		public static CancellationTokenSource cts;
		public static WebServer CreateWebServer()
		{
			var server = new WebServer(o => o
				.WithUrlPrefix("http://localhost:47561")
				.WithMode(HttpListenerMode.EmbedIO))
				.WithWebApi("/", m => m.WithController<PrimaryController>()
			);
			return server;
		}
	}

	class PrimaryController : WebApiController
	{
		[Route(HttpVerbs.Get, "/redirect")]
		public async Task Redirect()
		{
			var payload = $@"
				<html>
				<head>
				</head>
				<body>
				<script>
				        var hash = window.location.hash.substring(1);
				        var queryParams = new URLSearchParams(hash);
				        var accessToken = queryParams.get(""access_token"");
				        console.log(accessToken);

				        document.body.innerHTML += `<form id=""accessTokenForm"" action=""/success"" method=""post""><input type=""hidden"" name=""token"" value=""${{accessToken}}""></form>`;
				        document.getElementById(""accessTokenForm"").submit();
				</script>
				</body>
				</html>
			";
			HttpContext.Response.ContentType = "text/html";
			HttpContext.Response.Headers.Set("Content-Type", "text/html");
			HttpContext.SendStringAsync(payload, "text/html", Encoding.Default);
		}

		[Route(HttpVerbs.Post, "/success")]
		public async Task Login([FormField] string token)
		{
			Console.WriteLine($"TOKEN: {token}");
			var payload = $@"
				<html>
				<head>
				</head>
				<body>
					<div>
						<h2>You can now close this window.</h2>
					</div>
				</body>
				</html>
			";
			HttpContext.Response.ContentType = "text/html";
			HttpContext.Response.Headers.Set("Content-Type", "text/html");
			await HttpContext.SendStringAsync(payload, "text/html", Encoding.Default);

			Server.cts.Cancel();
			//Environment.Exit(0);
		}
	}
}
