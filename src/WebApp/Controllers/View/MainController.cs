using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers.View
{
	[Route("")]
	public class MainController : ViewControllerBase
	{
		[HttpGet("")]
		public IActionResult Index()
		{
			return this.View();
		}
	}
}
