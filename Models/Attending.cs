using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WeddingPlanner.Models
{
    public class Attending
    {
        [Key]
        public int AttendingId {get; set;}
        public int UserId {get; set;}
        public int WeddingId {get; set;}
        public MainUser attendingGuests {get; set;}

        public Wedding Weddings {get; set;}

    }
}