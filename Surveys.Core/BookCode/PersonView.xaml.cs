using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surveys.Core.BookCode
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonView : ContentPage
    {
        public PersonView()
        {
            InitializeComponent();

            timerButton.Clicked += (sender, e) => {
                //Timer.Text = "timer running on " + Device.OnPlatform("Android","iOS","Windows");
                Device.StartTimer(new TimeSpan(0, 0, 1), () => {
                    // do something every 10 seconds
                    Device.BeginInvokeOnMainThread(() => {
                        // interact with UI elements
                        Timer.Text = DateTime.Now.ToString("mm:ss") + " past the hour";
                    });
                    return true; // runs again, or false to stop
                });
            };

        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var person1 = (Person)Resources["person1"];
            person1.Name = "Sheila";
            person1.Country = "Argentina";
        }

        private async void ButtonTest_OnClicked(object sender, EventArgs e)
        {
            await Task.Delay(1000).ConfigureAwait(false);
            Device.BeginInvokeOnMainThread(() => myLabel.Text = DateTime.Now.ToString() + " | " + Device.Idiom + " | " + Device.RuntimePlatform);
        }
    }
}