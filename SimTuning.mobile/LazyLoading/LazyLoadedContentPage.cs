using System;
using Xamarin.Forms;

namespace SimTuning.mobile.LazyLoading
{
    internal class LazyLoadedContentPage : ContentPage, IActiveAware
    {
        public event EventHandler IsActiveChanged;

        private bool _isActive;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    IsActiveChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}