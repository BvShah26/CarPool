using DataAcessLayer.Models.Preferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Preference
{
    public interface IPreferenceSubType_Repo
    {
        Task<List<TravelPreference>> GetPrefrenceSubTypes();
        Task<TravelPreference> GetPrefrenceSubType_ById(int id);
        Task<List<TravelPreference>> Search_PrefrenceSubType(string SearchValue);

        Task<TravelPreference> Add_PrefrenceSubType(TravelPreference travelPreference);
        Task<TravelPreference> Update_PrefrenceSubType(int id, TravelPreference travelPreference);
        Task<TravelPreference> Delete_PrefrenceSubType(int id);

        Task<List<TravelPreference>> GetPreferencesByType(int TypeId);
        bool SubType_Exists(int id);
    }
}
