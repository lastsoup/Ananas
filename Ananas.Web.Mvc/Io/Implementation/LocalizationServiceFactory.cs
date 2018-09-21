using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Ananas.Web.Mvc.Io.Implementation
{
    /// <summary>
    /// 本地化实现类
    /// </summary>
    public class LocalizationServiceFactory
    {
        public LocalizationServiceFactory()
        {  
        }

        public Dictionary<string, string> Grid()
        {
            return CreateDictionary("GridLocalization", CultureInfo.CurrentCulture);
        }

        public Dictionary<string, string> Editor()
        {
            return CreateDictionary("EditorLocalization", CultureInfo.InstalledUICulture);
        }

        public Dictionary<string, string> Upload()
        {
            return CreateDictionary("UploadLocalization", CultureInfo.InstalledUICulture);
        }

        public Dictionary<string, string> CreateDictionary(string localization,CultureInfo culture)
        {
            LocalizationService localizationService = new LocalizationService(localization, culture);
            Dictionary<string, string> gridDictionary = localizationService.All().ToDictionary(k => k.Key[0].ToString(CultureInfo.CurrentCulture).ToLowerInvariant() + k.Key.Substring(1), k => k.Value);
            return gridDictionary;
        }
    }
}
