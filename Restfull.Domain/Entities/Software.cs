using System.ComponentModel.DataAnnotations;

namespace Restfull.Domain.Entities
{
    public class Software : Resource
    {
        [StringLength(50)]
        public string Version { get; set; } = string.Empty;

        [StringLength(100)]
        public string LicenseKey { get; set; } = string.Empty;

        public DateTime LicenseExpiryDate { get; set; }

        public override void ShowDetails()
        {
            Console.WriteLine($"Software Details - Version: {Version}, License Key: {LicenseKey}, " +
                            $"Expiry Date: {LicenseExpiryDate.ToShortDateString()}");
        }
    }
}