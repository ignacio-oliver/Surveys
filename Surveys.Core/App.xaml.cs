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
        }

        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync($"{nameof(LoginView)}");
            //await NavigationService.NavigateAsync($"{nameof(RootNavigationView)}/{nameof(SurveysView)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<RootNavigationView>();
            Container.RegisterTypeForNavigation<SurveysView>();
            Container.RegisterTypeForNavigation<SurveyDetailsView>();
            Container.RegisterTypeForNavigation<AboutView>();
            Container.RegisterTypeForNavigation<LoginView>();
            Container.RegisterTypeForNavigation<MainView>();
        }
    }
}