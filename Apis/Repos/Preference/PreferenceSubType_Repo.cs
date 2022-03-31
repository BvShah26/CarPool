using Apis.Data;
using Apis.Infrastructure.Preference;
using DataAcessLayer.Models.Preferences;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Preference
{
    public class PreferenceSubType_Repo : IPreferenceSubType_Repo
    {
        private readonly ApplicationDBContext _context;

        public PreferenceSubType_Repo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<TravelPreference> Add_PrefrenceSubType(TravelPreference travelPreference)
        {
            var result = await _context.TravelPreferences.AddAsync(travelPreference);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<TravelPreference> Delete_PrefrenceSubType(int id)
        {
            TravelPreference data = await GetPrefrenceSubType_ById(id);
            if (data != null)
            {
                _context.TravelPreferences.Remove(data);
                await _context.SaveChangesAsync();
            }

            return data;
        }

        public async Task<List<TravelPreference>> GetPrefrenceSubTypes()
        {
            return await _context.TravelPreferences.Include( item => item.Type).ToListAsync();
        }

        public async Task<TravelPreference> GetPrefrenceSubType_ById(int id)
        {
            return await _context.TravelPreferences.Include(item => item.Type).FirstOrDefaultAsync(item => item.Id == id);

        }

        public async Task<List<TravelPreference>> Search_PrefrenceSubType(string SearchValue)
        {
            List<TravelPreference> searchResult = await GetPrefrenceSubTypes();
            if (!String.IsNullOrEmpty(SearchValue))
            {
                searchResult = await _context.TravelPreferences.Where(item => item.Title.Contains(SearchValue) ||
                item.Type.Title.Contains(SearchValue)).ToListAsync();
            }
            return searchResult;
        }

        public bool SubType_Exists(int id)
        {
            return _context.TravelPreferences.Any(e => e.Id == id);
        }

        public async Task<TravelPreference> Update_PrefrenceSubType(int id, TravelPreference travelPreference)
        {
            TravelPreference record = await GetPrefrenceSubType_ById(id);
            if (record != null)
            {
                record.Title = travelPreference.Title;
                record.TypeId = travelPreference.TypeId;
            }
            _context.TravelPreferences.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }
    }
}
