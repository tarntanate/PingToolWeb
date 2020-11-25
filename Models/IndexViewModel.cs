using System;

namespace PingToolWeb.Models
{
	public class IndexViewModel
	{
		public string url { get; set; }
		public string error { get; set; }
		public long responseTime { get; set; }

	}
}