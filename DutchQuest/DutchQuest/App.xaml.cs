using System.Linq;
using Acr.UserDialogs;
using DutchQuest.Helpers;
using DutchQuest.Services;
using DutchQuest.Services.Abstractions;
using DutchQuest.Services.Mock;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DutchQuest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
#if DEBUG
            ServiceLocator.Instance.Add<IDataService, AppServiceData>();
#endif

            var dataService = ServiceLocator.Instance.Resolve<IDataService>();
            var topics = dataService.GetTopics().Result;
            MainPage = new Pages.GamePage(topics.First());
        }
    }
}