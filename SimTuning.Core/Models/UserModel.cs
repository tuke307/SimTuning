using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;

namespace SimTuning.Core.Models
{
    public class UserModel : MvxViewModel
    {
        private bool _userValid;

        public bool UserValid
        {
            get => _userValid;
            set => SetProperty(ref _userValid, value);
        }

        private bool _licenseValid;

        public bool LicenseValid
        {
            get => _licenseValid;
            set => SetProperty(ref _licenseValid, value);
        }

        public WordPressPCL.Models.User User { get; set; }
        public WooCommerceNET.WooCommerce.Legacy.Order Order { get; set; }
    }
}