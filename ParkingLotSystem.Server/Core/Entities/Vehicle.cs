using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotSystem.Server.Core.Entities
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string LicensePlate { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string OwnerName { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string ApartmentNumber { get; set; }

        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }

        public int SiteID { get; set; }

        //[ForeignKey("SiteID")]
        //public virtual Site Site { get; set; }
    }
}
