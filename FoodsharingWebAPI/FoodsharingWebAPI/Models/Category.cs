﻿using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Announcement>? Announcements { get; set; } 
    }
}
