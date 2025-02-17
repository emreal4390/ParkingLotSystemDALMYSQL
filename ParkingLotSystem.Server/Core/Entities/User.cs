using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotSystem.Server.Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string FullName { get; set; }

        [Required]
        [Column(TypeName = "varchar(32)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Role { get; set; }

        public int SiteID { get; set; }

        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }


    }
}
