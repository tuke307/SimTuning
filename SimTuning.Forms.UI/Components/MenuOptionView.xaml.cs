// project=SimTuning.Forms.UI, file=MenuOptionView.xaml.cs, creation=2020:7:2
// Copyright (c) 2020 tuke productions. All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.Components
{
    public partial class MenuOptionView : StackLayout
    {
        public MenuOptionView()
        {
            this.InitializeComponent();
        }

        public string Text
        {
            set
            {
                Label.Text = value;
            }
        }

        public string Source
        {
            set
            {
                Icon.Source = value;
            }
        }
    }
}