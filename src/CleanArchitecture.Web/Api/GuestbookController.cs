using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArchitecture.Web.Api
{
    [Route("api/[controller]")]
    public class GuestbookController : Controller
    {
        private readonly IRepository<Guestbook> _guestbookRepository;
        public GuestbookController(IRepository<Guestbook> guestbookRepository)
        {
            _guestbookRepository = guestbookRepository;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var guestbook = _guestbookRepository.GetById(id);
            if (guestbook == null)
            {
                return NotFound(id);
            }
            return Ok(guestbook);
        }

        // POST api/values
        [HttpPost("{id:int}/NewEntry")]
        public async Task<IActionResult> NewEntry(int id, [FromBody]GuestbookEntry entry)
        {
            var guestbook = _guestbookRepository.GetById(id);
            guestbook.AddEntry(entry);
            _guestbookRepository.Update(guestbook);
            return Ok(guestbook);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
