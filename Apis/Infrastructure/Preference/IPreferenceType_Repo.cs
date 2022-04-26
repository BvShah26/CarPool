using DataAcessLayer.Models.Preferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Preference
{
    public interface IPreferenceType_Repo
    {
        Task<List<PreferenceType>> GetPreferenceTypes();
        Task<PreferenceType> GetPreferenceType_ById(int id);
        Task<List<PreferenceType>> Search_PreferenceType(string SearchValue);

        

        Task<PreferenceType> Add_PreferenceType(PreferenceType PreferenceType);
        Task<PreferenceType> Update_PreferenceType(int id, PreferenceType PreferenceType);
        Task<PreferenceType> Delete_PreferenceType(int id);
        bool Type_Exists(int id);
    }
}
