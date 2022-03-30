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
    public class PreferenceType_Repo : IPreferenceType_Repo
    {
        private readonly ApplicationDBContext _context;

        public PreferenceType_Repo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<PreferenceType> Add_PreferenceType(PreferenceType PreferenceType)
        {
            var result = await _context.PreferenceTypes.AddAsync(PreferenceType);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<PreferenceType> Delete_PreferenceType(int id)
        {
            PreferenceType data = await GetPreferenceType_ById(id);
            if (data != null)
            {
                _context.PreferenceTypes.Remove(data);
                await _context.SaveChangesAsync();
            }

            return data;
        }

        public async Task<List<PreferenceType>> GetPreferenceTypes()
        {
            return await _context.PreferenceTypes.ToListAsync();

        }

        public async Task<PreferenceType> GetPreferenceType_ById(int id)
        {
            return await _context.PreferenceTypes.FindAsync(id);

        }

        public async Task<List<PreferenceType>> Search_PreferenceType(string SearchValue)
        {
            List<PreferenceType> searchResult = await GetPreferenceTypes();
            if (!String.IsNullOrEmpty(SearchValue))
            {
                searchResult = await _context.PreferenceTypes.Where(item => item.Title.Contains(SearchValue)).ToListAsync();
            }
            return searchResult;
        }

        public bool Type_Exists(int id)
        {
            return _context.PreferenceTypes.Any(e => e.Id == id);

        }

        public async Task<PreferenceType> Update_PreferenceType(int id, PreferenceType PreferenceType)
        {
            PreferenceType record = await GetPreferenceType_ById(id);
            if (record != null)
            {
                record.Title = PreferenceType.Title;
            }
            _context.PreferenceTypes.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }
    }
}
