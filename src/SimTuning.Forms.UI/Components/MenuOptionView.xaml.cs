// Copyright (c) 2021 tuke productions. All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.Components
{
    public partial class MenuOptionView : StackLayout
    {
        public ImageSource Source
        {
            set
            {
                Icon.Source = value;
            }
        }

        public string Text
        {
            set
            {
                Label.Text = value;
            }
        }

        public MenuOptionView()
        {
            this.InitializeComponent();
        }
    }
}