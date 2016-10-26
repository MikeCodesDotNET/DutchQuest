using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchQuest.Helpers;
using DutchQuest.Models;
using DutchQuest.Services.Abstractions;
using Xamarin.Forms;

namespace DutchQuest.ViewModels
{
    public class TopicViewModel : BaseViewModel
    {
        private Topic topic;
        public ObservableCollection<Word> Words { get; private set; }

        public TopicViewModel(Topic topic)
        {
            Words = new ObservableCollection<Word>();
            this.topic = topic;
            Refresh();
        }

        public Color LabelColor => Colors.OuterSpace;

        Command refreshCommand;
        public Command RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }
        
        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var words = topic.Words;
                Words.Clear();

                foreach (var word in words)
                {
                    Words.Add(word);
                }
            }
            catch (Exception ex)
            {
                //Acr.UserDialogs.UserDialogs.Instance.ShowError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        void Refresh()
        {
            ExecuteRefreshCommand();
            MessagingCenter.Subscribe<TopicViewModel>(this, "ItemsChanged", (sender) =>
            {
                ExecuteRefreshCommand();
            });
        }
    }
}
