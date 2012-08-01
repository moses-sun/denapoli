using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Timers;

namespace Denapoli.Modules.Data.DataProvider
{
    [Export(typeof(ISettingsService))]
    public class SettingsService : ISettingsService
    {
        private const string Server = "server";
        private const string UserId = "userid";
        private const string Password = "password";
        private const string Database = "database";
        private const string FilesRepository = "filesrepository";
        private const string BorneId = "borneid";

        private Dictionary<string, string> Settings { get; set; }

        [ImportingConstructor]
        public SettingsService()
        {
           LoadSettings();
            var timer = new Timer { Interval = 60000 };
            timer.Elapsed += (sender, args) => LoadSettings(); 
            timer.Enabled = true;
            timer.Start();
        }

        public string GetDbConnextionParameters()
        {
            return String.Format("server={0};user id={1}; password={2}; database={3}", Settings[Server], Settings[UserId], Settings[Password], Settings[Database]);
        }

        public string GetDataRepositoryRootPath()
        {
            return Settings[FilesRepository];
        }

        public int GetBorneId()
        {
            return Int32.Parse(Settings[BorneId]);
        }


        private void LoadSettings()
        {
            var settings = new Dictionary<string, string>();
            var source = new StreamReader(File.Open("settings.txt", FileMode.Open));
            string line;
            while ((line = source.ReadLine()) != null)
            {
                var line2 = line.Split(new[] {'='});
                settings[line2[0].ToLower()] = line2[1];
            }
            Settings = settings;
        }
    }
}
