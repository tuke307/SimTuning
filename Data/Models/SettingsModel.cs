using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class SettingsModel : BaseEntityModel
    {
        [Required]
        public bool DarkMode { get; set; }

        [Required]
        public string PrimaryColor { get; set; }

        [Required]
        public string SecondaryColor { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }
    }
}