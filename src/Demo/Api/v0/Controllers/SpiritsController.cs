using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Demo.Models.DB;
using Demo.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Api.v0.Controllers
{
    [Route("api/v0/[controller]")]
    public class SpiritsController : Controller
    {
		[HttpGet]
		public IEnumerable<Spirit> Get(SpiritRepository spiritRepository)
		{
			return spiritRepository.GetAll();
		}
	}
}
