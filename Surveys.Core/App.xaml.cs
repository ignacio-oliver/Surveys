using Surveys.Core.BookCode;
using Surveys.Core.Views;
using Xamarin.Forms.Xaml;
using Prism.Unity;

namespace Surveys.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();

            /* Forma en que se inicia la aplicación utilizando Xamarin.Forms (Heredando de Application) */

            /* Inicia proyecto : Libro */
            //MainPage = new NavigationPage(new SurveysView());

            /* Inicia test's */
            //MainPage = new NavigationPage(new PersonView());
        }

        protected override async void OnInitialized()
        {
            /* Forma en que se inicia la aplicación utilizando Prism (Heredando de PrismApplication) */

            await NavigationService.NavigateAsync($"{nameof(RootNavigationView)}/{nameof(SurveysView)}");

            //await NavigationService.NavigateAsync($"{nameof(RootNavigationView)}/{nameof(PersonView)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<RootNavigationView>();
            Container.RegisterTypeForNavigation<SurveysView>();
            Container.RegisterTypeForNavigation<SurveyDetailsView>();

            //Container.RegisterTypeForNavigation<PersonView>();
        }
    }
}