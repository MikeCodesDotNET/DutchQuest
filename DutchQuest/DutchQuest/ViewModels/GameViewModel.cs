using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DutchQuest.Models;
using DutchQuest.Services.Abstractions;
using Plugin.Media;
using Xamarin.Forms;

namespace DutchQuest.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private Topic topic;

        public GameViewModel(Topic topic)
        {
            this.topic = topic;
            NextWordIsVisible = false;
            ImageVisible = false;

            SnapPhotoIsVisible = true;
            NextWordIsVisible = true;
            SetNextWord();
        }

        public string DutchWord { get; private set; }
        public bool TextVisible { get; private set; }
        public bool NextWordIsVisible { get; private set; }
        public bool SnapPhotoIsVisible { get; private set; }
        public bool ImageVisible { get; private set; }

        public ImageSource Photo { get; set; }
        
        private int position;
        private Word word;
        public void SetNextWord()
        {
            word = topic.Words[position];
            if (word != null)
            {
                DutchWord = word.Dutch;
                TextVisible = true;
                ImageVisible = false;
                SnapPhotoIsVisible = true;
                OnPropertyChanged($"TextVisible");
                OnPropertyChanged($"DutchWord");
                OnPropertyChanged($"ImageVisible");
                OnPropertyChanged($"SnapPhotoIsVisible");
            }
            position++;
        }

        public async Task TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            //TextVisible = false;
            ImageVisible = true;
            SnapPhotoIsVisible = false;
            OnPropertyChanged($"TextVisible");
            OnPropertyChanged($"ImageVisible");
            OnPropertyChanged($"SnapPhotoIsVisible");


            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "images",
                Name = "test.jpg"
            });

            if (file == null)
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowError("No Image");
                return;
            }
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Looking at the photo");

            TextVisible = false;

            Photo = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            OnPropertyChanged($"Photo");

            var cogs = new Services.Azure.CognitiveService();
            var descriptions = await cogs.GetImageDescription(file.GetStream());
            bool matchFound;
            foreach (var tag in descriptions.Description.Tags)
            {
                if (tag.Contains(word.English))
                {
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    Acr.UserDialogs.UserDialogs.Instance.ShowSuccess("Correct!");
                    matchFound = true;
                }
            }

            Acr.UserDialogs.UserDialogs.Instance.HideLoading();

        }

        ICommand setNextWordCommand;
        public ICommand SetNextWordCommand => setNextWordCommand ?? (setNextWordCommand = new Command(SetNextWord));

        ICommand takePhotoCommand;
        public ICommand TakePhotoCommand => takePhotoCommand ?? (takePhotoCommand = new Command(async () => TakePhoto()));


    }
}
