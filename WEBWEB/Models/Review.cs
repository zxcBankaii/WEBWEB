﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WEBWEB.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; } 
    }
}
