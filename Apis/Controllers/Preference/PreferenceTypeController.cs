using Apis.Infrastructure.Preference;
using DataAcessLayer.Models.Preferences;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apis.Controllers.Preference
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceTypeController : ControllerBase
    {

        private readonly IPreferenceType_Repo _preferenceType_Repo;

        public PreferenceTypeController(IPreferenceType_Repo preferenceType_Repo)
        {
            _preferenceType_Repo = preferenceType_Repo;
        }


        // GET: api/<PreferenceTypeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreferenceType>>> GetPreferenceTypes()
        {
            return await _preferenceType_Repo.GetPreferenceTypes();
        }


        // GET api/<PreferenceTypeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PreferenceType>> GetPreferenceType(int id)
        {
            var preferenceType = await _preferenceType_Repo.GetPreferenceType_ById(id);

            if (preferenceType == null)
            {
                return NotFound();
            }

            return preferenceType;
        }


        [HttpGet("Search/{SearchValue}")]
        public async Task<List<PreferenceType>> Search(string SearchValue)
        {
            return await _preferenceType_Repo.Search_PreferenceType(SearchValue);
        }


        // POST api/<PreferenceTypeController>
        [HttpPost]
        public async Task<ActionResult<PreferenceType>> PostPreferenceType(PreferenceType PreferenceType)
        {
            await _preferenceType_Repo.Add_PreferenceType(PreferenceType);

            return CreatedAtAction("GetPreferenceType", new { id = PreferenceType.Id }, PreferenceType);
        }


        // PUT api/<PreferenceTypeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreferenceType(int id, PreferenceType PreferenceType)
        {

            if (id != PreferenceType.Id)
            {
                return BadRequest();
            }


            try
            {
                await _preferenceType_Repo.Update_PreferenceType(id, PreferenceType);
                return CreatedAtAction("GetPreferenceType", new { id = PreferenceType.Id }, PreferenceType);
            }
            catch (Exception)
            {
                if (!_preferenceType_Repo.Type_Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }


        // DELETE api/<PreferenceTypeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PreferenceType>> DeletePreferenceType(int id)
        {
            var preferenceType = await _preferenceType_Repo.Delete_PreferenceType(id);
            if (preferenceType == null)
            {
                return NotFound();
            }
            return preferenceType;
        }
    }
}
