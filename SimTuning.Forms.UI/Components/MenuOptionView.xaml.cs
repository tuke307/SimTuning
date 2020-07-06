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
            InitializeComponent();
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