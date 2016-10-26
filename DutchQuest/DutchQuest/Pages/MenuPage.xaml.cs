using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DutchQuest.Pages
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();

            BindingContext = new ViewModels.MenuPageViewModel();
        }
    }
}
