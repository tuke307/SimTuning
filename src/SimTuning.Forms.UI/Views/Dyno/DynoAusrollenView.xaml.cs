﻿// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Dyno;

namespace SimTuning.Forms.UI.Views.Dyno
{
    [MvxModalPresentation]
    public partial class DynoAusrollenView : MvxContentPage<DynoAusrollenViewModel>
    {
        public DynoAusrollenView()
        {
            InitializeComponent();
        }
    }
}