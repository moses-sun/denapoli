using System;
using System.ComponentModel.Composition;

namespace Denapoli.Modules.Data.DataProvider
{
    [Export(typeof(ISettingsService))]
    public class SettingsService : ISettingsService
    {
        public string GetDbConnextionParameters()
        {
            return String.Format("server={0};user id={1}; password={2}; database={3}", "localhost", "root", "", "denapoli");
        }

        public string GetDataRepositoryRootPath()
        {
            return "http://127.0.0.1:8080/";
        }
    }
}
