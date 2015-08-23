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
    [Route("api/v0/[controller]/[action]")]
    public class SpiritsController : Controller
    {
		[HttpGet]
		public IEnumerable<Spirit> Get(SpiritRepository spiritRepository)
		{
			return spiritRepository.GetAll();
		}

	    [HttpGet]
	    public Spirit Add(SpiritRepository spiritRepository)
	    {
		    var spirit = new Spirit
		    {
			    Name = "yeee"
		    };
		    var x = spiritRepository.Add(spirit);
		    return x;
	    }
    }
}
