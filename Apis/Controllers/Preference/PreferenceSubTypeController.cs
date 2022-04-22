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
    public class PreferenceSubTypeController : ControllerBase
    {

        private readonly IPreferenceSubType_Repo _Repo;

        public PreferenceSubTypeController(IPreferenceSubType_Repo Repo)
        {
            _Repo = Repo;
        }

        // GET: api/<PreferenceSubTypeController>
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<TravelPreference>>> GetSubTypes()
        {
            return await _Repo.GetPrefrenceSubTypes();
        }

        // GET api/<PreferenceSubTypeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TravelPreference>> GetSubTypes_Id(int id)
        {
            var travelPreference = await _Repo.GetPrefrenceSubType_ById(id);

            if (travelPreference == null)
            {
                return NotFound();
            }

            return travelPreference;
        }


        [HttpGet("Type/{TypeId}")]
        public async Task<IActionResult> Type(int TypeId)
        {
            var Preferences = await _Repo.GetPreferencesByType(TypeId);
            return Ok(Preferences);
        }


        [HttpGet("Search/{SearchValue}")]
        public async Task<List<TravelPreference>> Search(string SearchValue)
        {
            return await _Repo.Search_PrefrenceSubType(SearchValue);
        }


        // POST api/<PreferenceSubTypeController>
        [HttpPost]
        public async Task<ActionResult<TravelPreference>> PostPreferenceType(TravelPreference travelPreference)
        {
            await _Repo.Add_PrefrenceSubType(travelPreference);

            return CreatedAtAction("GetSubTypes", new { id = travelPreference.Id }, travelPreference);
        }


        // PUT api/<PreferenceSubTypeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreferenceType(int id, TravelPreference TravelPreference)
        {

            if (id != TravelPreference.Id)
            {
                return BadRequest();
            }


            try
            {
                await _Repo.Update_PrefrenceSubType(id, TravelPreference);
                return CreatedAtAction("GetSubTypes", new { id = TravelPreference.Id }, TravelPreference);
            }
            catch (Exception)
            {
                if (!_Repo.SubType_Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }


        // DELETE api/<PreferenceSubTypeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TravelPreference>> DeletePreferenceType(int id)
        {
            var travelPreference = await _Repo.Delete_PrefrenceSubType(id);
            if (travelPreference == null)
            {
                return NotFound();
            }
            return travelPreference;
        }
    }
}
