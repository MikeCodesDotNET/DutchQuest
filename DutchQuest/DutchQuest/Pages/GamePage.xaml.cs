using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchQuest.Models;
using Xamarin.Forms;

namespace DutchQuest.Pages
{
    public partial class GamePage : ContentPage
    {
        public GamePage(Topic topic)
        {
            InitializeComponent();
            BindingContext = new ViewModels.GameViewModel(topic);
        }
    }
}
