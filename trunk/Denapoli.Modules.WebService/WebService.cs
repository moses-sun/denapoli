using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.WebService
{
    [Export(typeof(IWebService))]
    public class WebService : NotifyPropertyChanged,IWebService
    {
        public ISettingsService SettingsService { get; set; }

        [ImportingConstructor]
        public WebService(ISettingsService settingsService)
        {
            SettingsService = settingsService;
        }


        public bool UploadFile(string remoteurl, string localurl)
        {
            if (!File.Exists(localurl)) return false;
            var client = new WebClient();
            try
            {
                client.UploadFile(remoteurl, "POST", localurl);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }



        public bool DownloadFile(string remoteurl, string localurl)
        {
            try
            {
                var responseStream = DownloadFile(remoteurl);
                if (responseStream == null) return false;

                if (File.Exists(localurl))
                    File.Delete(localurl);

                var output = File.Create(localurl);
                responseStream.CopyTo(output);
                output.Close();
                responseStream.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

         public Stream DownloadFile(string remoteurl)
         {
             try
             {
                 var request = WebRequest.Create(new Uri(remoteurl, UriKind.Absolute));
                 request.Timeout = 1000*15;
                 var stream = request.GetResponse().GetResponseStream();
                 return stream;
             }
             catch (Exception)
             {
                 return null;
             }
         }
    }
}