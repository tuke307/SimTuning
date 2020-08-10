// project=SimTuning.Forms.WPFCore, file=ModifierBase.cs, creation=2020:8:4
// Copyright (c) 2020 tuke productions. All rights reserved.
using System.Windows;

namespace SimTuning.Forms.WPFCore.Menu
{
    public abstract class ModifierBase
    {
        public abstract void Apply(DependencyObject target);
    }
}