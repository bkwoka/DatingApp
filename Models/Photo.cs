﻿using System;
using System.Reflection.Metadata.Ecma335;

namespace DatingApp.Models
 {
     public class Photo
     {
         public int Id { get; set; }
         public string Url { get; set; }
         public string Description { get; set; }
         public DateTime DateAdded { get; set; }
         public string PublicId { get; set; }
         public bool IsMain { get; set; }
         public User User { get; set; }
         public int UserId { get; set; }
     }
 } 