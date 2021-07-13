using System;
using System.Web;

namespace Rhetos.FloydExtensions
{
	public class CrossOriginSupportModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.BeginRequest += ContextOnBeginRequest;
		}
		private void ContextOnBeginRequest(object sender, EventArgs eventArgs)
		{
			var flag = HttpContext.Current.Request.HttpMethod == "OPTIONS";
			if (flag)
			{
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.SuppressContent = true;
				HttpContext.Current.ApplicationInstance.CompleteRequest();
			}
		}
		public void Dispose()
		{
		}
	}
}