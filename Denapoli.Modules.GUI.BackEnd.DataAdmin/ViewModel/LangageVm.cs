using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    public class LangageVm : NotifyPropertyChanged, IEditableObject
    {
        public Langage Langage { get; set; }
        public ActionCommand BrowseImageCommand { get; set; }

        public LangageVm()
        {
            Langage = new Langage();
            ReSetProperties();
            BrowseImageCommand = new ActionCommand(BrowseImage);
        }

        public LangageVm(Langage l)
        {
            Langage = l;
            ReSetProperties();
        }

        private void BrowseImage()
        {
            var chooser = new OpenFileDialog { Filter = "Image files (*.png, *.jpg)|*.png;*.jpg" };
            var res = chooser.ShowDialog();
            if (DialogResult.Cancel.Equals(res))
                return;
            ImageURL = chooser.FileName;
        }

        private void ReSetProperties()
        {
            Nom = Langage.Name;
            Code = Langage.Code;
            ImageURL = Langage.ImageURL;
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

        private string _oldPrenom;
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



        public void BeginEdit()
        {
            _oldNom = Nom;
            _oldPrenom = Code;

        }

        private void UpdateProduit()
        {
           
        }

        public void EndEdit()
        {
            UpdateProduit();
        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Code = _oldPrenom;
        }
    }
}