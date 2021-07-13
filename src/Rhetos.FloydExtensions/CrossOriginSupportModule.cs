﻿/*
    Copyright (C) 2014 Omega software d.o.o.

    This file is part of Rhetos.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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