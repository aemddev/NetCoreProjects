using BloodBankManagement.Server.Services;
using BloodBankManagement.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Collections.Generic;

namespace BloodBankManagement.Server.Controllers
{
	[Route("api/[Controller]")]
	[ApiController]
	public class DonerController : ControllerBase
	{
		private readonly IDoner iDoner;
		public DonerController(IDoner iDoner)
		{
			this.iDoner = iDoner;
		}
		//take all doner on the db
		[HttpGet]
		public async Task<List<Doner>> DonerList()
		{
			return await Task.FromResult(iDoner.DataDoner());
		}
		//take a list who a person can donate
		[HttpGet("{Id}/CanDonate")]
		public async Task<List<Doner>> ReceiversList(int id)
		{
			Doner doner = iDoner.GetDoner(id);
			return await Task.FromResult(iDoner.CanDonateTo(doner));
		}
        //take a list who a person can receive
        [HttpGet("{Id}/CanReceive")]
		public async Task<List<Doner>> DonorsList(int id)
		{
			Doner doner = iDoner.GetDoner(id);
			return await Task.FromResult(iDoner.CanReceiveFrom(doner));
		}
		//create a new doner to db
		[HttpPost]
		public void PostDoner(Doner doner)
		{
			iDoner.NewDoner(doner);
		}
		//get all data from one person
		[HttpGet("{Id}")]
		public IActionResult GetDonerData(int id) 
		{
			Doner doner = iDoner.GetDoner(id);
			if(doner != null)
			{
				return Ok(doner);
			}
			else
			{
				return NotFound();
			}
		}
		//edit one doner
		[HttpPut]
		public void EditDoner(Doner doner)
		{
			iDoner.UpdateDoner(doner);
		}
		//delete one doner
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			iDoner.DeleteDoner(id);
			return Ok();
		}
    }
}
