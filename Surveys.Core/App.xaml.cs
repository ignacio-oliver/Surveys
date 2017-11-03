using Surveys.Core.Views;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Services;
using Microsoft.Practices.Unity;

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

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //Container.RegisterInstance(typeof(ILocalDbService), "dbConnection", new LocalDbService(), null);

            Container.RegisterInstance<ILocalDbService>(new LocalDbService());
            Container.RegisterInstance<IWebApiService>(new WebApiService());

        }
    }
}