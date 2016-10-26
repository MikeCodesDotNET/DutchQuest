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
    public class MenuPageViewModel : BaseViewModel
    {
        public ObservableCollection<Topic> Topics { get; private set; }
        private IDataService dataService;

        public MenuPageViewModel()
        {
            Topics = new ObservableCollection<Topic>();

            dataService = ServiceLocator.Instance.Resolve<IDataService>();
            if (dataService == null)
                return;


            var topics = dataService.GetTopics().Result;
            foreach (var topic in topics)
            {
                Topics.Add(topic);
            }
        }

        public Color LabelColor => Colors.Blue;


        Topic selectedTopic;
        public Topic SelectedTopic
        {
            get { return selectedTopic; }
            set
            {
                selectedTopic = value;
                OnPropertyChanged("SelectedItem");

                if (selectedTopic == null) return;

                var navigation = Application.Current.MainPage as NavigationPage;
                var menu = navigation.CurrentPage as MasterDetailPage;
                SelectedTopic = null;
            }
        }

    }
}

