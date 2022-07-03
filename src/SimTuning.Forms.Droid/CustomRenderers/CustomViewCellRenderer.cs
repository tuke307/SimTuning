// Copyright (c) 2021 tuke productions. All rights reserved.
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using SimTuning.Forms.Droid.CustomRenderers;
using SimTuning.Forms.UI.Components;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]

namespace SimTuning.Forms.Droid.CustomRenderers
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View _cellCore;
        private bool _selected;
        private Drawable _unselectedBackground;

        protected override Android.Views.View GetCellCore(
            Cell item,
            Android.Views.View convertView,
            ViewGroup parent,
            Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);

            _selected = false;
            _unselectedBackground = _cellCore.Background;

            return _cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);

            if (args.PropertyName == "IsSelected")
            {
                _selected = !_selected;

                if (_selected)
                {
                    var extendedViewCell = sender as CustomViewCell;
                    _cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
                }
                else
                {
                    _cellCore.SetBackground(_unselectedBackground);
                }
            }
        }
    }
}