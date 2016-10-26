using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchQuest.Models;
using Xamarin.Forms;

namespace DutchQuest.Pages
{
    public partial class TopicPage : ContentPage
    {
        public TopicPage(Topic topic)
        {
            InitializeComponent();
            BindingContext = new ViewModels.TopicViewModel(topic);
        }
    }
}
