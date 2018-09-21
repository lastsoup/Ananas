using System.Collections.Generic;

namespace Ananas.Web.Mvc.Io
{
    internal interface ILocalizationService
    {
        string One(string key);

        IDictionary<string, string> All();

        bool IsDefault
        {
            get;
        }
    }
}
