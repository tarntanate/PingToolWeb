using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PingToolWeb.Models;

namespace PingToolWeb.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			var model = new IndexViewModel { url = string.Empty, responseTime = 0, error = string.Empty };
			return View(model);
		}

		[HttpPost]
		public IActionResult Index([FromForm] string url)
		{
			bool pingable = false;
			Ping pinger = null;
			long RoundtripTime = 0;
			string error = String.Empty;
			try
			{
				pinger = new Ping();
				PingReply reply = pinger.Send(url);
				RoundtripTime = reply.RoundtripTime;
				// pingable = reply.Status == IPStatus.Success;
			}
			catch (PingException ex)
			{
				// Discard PingExceptions and return false;
				error = ex.Message;
				if (ex.InnerException != null)
				{
					error += ": " + ex.InnerException.Message;
				}
			}
			finally
			{
				if (pinger != null)
				{
					pinger.Dispose();
				}
			}

			var model = new IndexViewModel { url = url, responseTime = RoundtripTime, error = error };
			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
