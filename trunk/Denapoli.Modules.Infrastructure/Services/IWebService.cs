using System.ComponentModel;
using System.IO;

namespace Denapoli.Modules.Infrastructure.Services
{
    public interface IWebService : INotifyPropertyChanged
    {
        bool UploadFile(string remoteurl, string localurl);
        bool DownloadFile(string remoteurl, string localurl);
        Stream DownloadFile(string remoteurl);
    }
}