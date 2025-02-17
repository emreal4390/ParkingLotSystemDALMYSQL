using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ParkingLotSystem.Server.Core.Entities;

public class Site
{
    public int SiteID { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string SiteName { get; set; }

    [Required]
    [Column(TypeName = "varchar(36)")]
    public string SiteSecret { get; set; }

    [Required]
    public int ClientID { get; set; } // 🔥 Artık her site bir Client'a ait

    [Required]
    [Column(TypeName = "varchar(36)")]
   

    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Vehicle> Vehicles { get; set; }
}
