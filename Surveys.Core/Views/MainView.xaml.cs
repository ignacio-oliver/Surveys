﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surveys.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : MasterDetailPage
    {
        public MainView()
        {
            /* UWP : Hamburguesa visible */
            if (Device.RuntimePlatform == Device.Windows)
            {
                MasterBehavior = MasterBehavior.Popover;
            }

            InitializeComponent();
        }
    }
}