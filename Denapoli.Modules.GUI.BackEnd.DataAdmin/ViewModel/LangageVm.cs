using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    public class LangageVm : NotifyPropertyChanged, IEditableObject
    {
        public Langage Langage { get; set; }
        public static IDataProvider DataProvider { get; set; }
        public static ISettingsService SettingsService { get; set; }
        public ActionCommand BrowseImageCommand { get; set; }
        public static IUpdatebale Parent { get; set; }


        public LangageVm()
        {
            Langage = new Langage{Code = "",Name = ""};
            BrowseImageCommand = new ActionCommand(BrowseImage);
            ReSetProperties();
            LanguagesAdminViewModel.Keys.ForEach(item => Dico.Add(new DicoEntry { Key = item, Value = "" }));
        }

        public LangageVm(Langage l,IDataProvider dataProvider, ISettingsService settingsService)
        {
            Langage = l;
            DataProvider = dataProvider;
            BrowseImageCommand = new ActionCommand(BrowseImage);
            SettingsService = settingsService;
            ReSetProperties();
        }

        private string _imageLocalURL;
        public string ImageLocalURL
        {
            get
            {
                return _imageLocalURL;
            }
            set
            {
                _imageLocalURL = value;
                NotifyChanged("ImageLocalURL");
            }
        }


        private void BrowseImage()
        {
            var chooser = new OpenFileDialog { Filter = "Image files (*.png, *.jpg)|*.png;*.jpg" };
            var res = chooser.ShowDialog();
            if (DialogResult.Cancel.Equals(res))
                return;
            ImageLocalURL = chooser.FileName;
            ImageURL = Path.GetFileName(ImageLocalURL);
            IsImageLoaded = Visibility.Visible;
            IsPodImage = Visibility.Collapsed;
        }

        private void ReSetProperties()
        {
            Nom = Langage.Name;
            Code = Langage.Code;
            ImageURL = Langage.ImageURL;
            Dico = new ObservableCollection<DicoEntry>();
            Langue = DataProvider.GetAvailableLanguages().FirstOrDefault(item => item.Code==Langage.Code) ??
                     new Langue{Code = "oo", NoM = "oo"};
            IsImageLoaded = Visibility.Collapsed;
            IsPodImage = Visibility.Visible;
        }

        private Langue _langue;
        public Langue Langue
        {
            get { return _langue; }
            set
            {
                _langue = value;
                NotifyChanged("Langue");
            }
        }

        private string _oldNom;
        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set
            {
                _nom = value;
                NotifyChanged("Nom");
            }
        }

        private string _oldCode;
        private string _code;

        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                NotifyChanged("Code");
            }
        }

        private string _oldImageURL;
        private string _imageURL;
        public string ImageURL
        {
            get { return _imageURL; }
            set
            {
                _imageURL = value;
                NotifyChanged("ImageURL");
                IsImageLoaded = Visibility.Visible;
                IsPodImage = Visibility.Collapsed;
            }
        }


        private Visibility _isImageLoaded;
        public Visibility IsImageLoaded
        {
            get { return _isImageLoaded; }
            set
            {
                _isImageLoaded = value;
                NotifyChanged("IsImageLoaded");
            }
        }

        private Visibility _isPodImage;
        public Visibility IsPodImage
        {
            get { return _isPodImage; }
            set
            {
                _isPodImage = value;
                NotifyChanged("IsPodImage");
            }
        }

        public ObservableCollection<DicoEntry> Dico { get; set; }

        public void BeginEdit()
        {
            _oldNom = Nom;
            _oldCode = Code;
            _oldImageURL = ImageURL;
            Dico.ForEach(item => item.BeginEdit());
        }

        private void UpdateLangue()
        {
            Langage.Name = Nom;
            Langage.Code = Code;
            Langue.Code = Code;
            Langue.NoM = Nom;
        }

        public void EndEdit()
        {
            UpdateLangue();
            Langue = DataProvider.InsertIfNotExists(Langue);
            if (IsImageLoaded == Visibility.Visible)
                UploadFile();
            SendDico();
            DataAdminViewModel.EventAggregator.GetEvent<UpdateEvent>().Publish(Parent);
        }

        private void UploadFile()
        {
            if (!File.Exists(ImageLocalURL)) return;
            var destName = Code + ".png";// +ImageLocalURL.Split('.')[1];
            File.Copy(ImageLocalURL, destName);
            var client = new WebClient();
            client.UploadFile(SettingsService.GetDataRepositoryRootPath() + "images/upload.php", "POST", destName);
            File.Delete(destName);
            ImageURL = destName;
        }

        private void SendDico()
        {
            DumpFile(Code+".txt");
            var client = new WebClient();
            client.UploadFile(SettingsService.GetDataRepositoryRootPath() + "i18n/upload.php", "POST",Code+".txt" );
        }

        private void DumpFile(string s)
        {
            var lines = new string[Dico.Count];
            var i = 0;
            foreach (var entry in Dico)
                lines[i++] = entry.Key + "=" + entry.Value;
            File.WriteAllLines(s, lines);
        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Code = _oldCode;
            ImageURL = _oldImageURL;
            IsImageLoaded = Visibility.Collapsed;
            IsPodImage = Visibility.Visible;
            Dico.ForEach(item => item.CancelEdit());
        }
    }
}