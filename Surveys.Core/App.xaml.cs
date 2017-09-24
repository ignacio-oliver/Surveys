using Surveys.Core.BookCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surveys.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            /* Inicia proyecto : Libro */
            //MainPage = new NavigationPage(new SurveysView());

            /* Inicia test's */
            MainPage = new NavigationPage(new PersonView());
        }
    }
}