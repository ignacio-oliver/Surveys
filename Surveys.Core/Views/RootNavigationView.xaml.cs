using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surveys.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootNavigationView : NavigationPage
    {
        public RootNavigationView()
        {
            InitializeComponent();
        }
    }
}