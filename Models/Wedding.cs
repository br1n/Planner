using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId {get; set;}

        [Required]
        public string Bride {get; set;}

        [Required]
        public string Groom {get; set;}

        [Required(ErrorMessage="You must have a wedding date?!")]
        [DataType(DataType.Date)]
        public DateTime Date {get; set;}

        [Required]
        public string Address {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;

        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        public int UserId {get; set;}
        public MainUser WeddingCreator {get; set;}

        public List<Attending> Attendees { get; set; }

    }
}
