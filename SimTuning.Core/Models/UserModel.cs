using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;

namespace SimTuning.Core.Models
{
    public class UserModel
    {
        public bool UserValid { get; set; }

        public bool LicenseValid { get; set; }

        public WordPressPCL.Models.User? User { get; set; }
        public WooCommerceNET.WooCommerce.Legacy.Order? Order { get; set; }
    }
}