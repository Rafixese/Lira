using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Lira.Models
{
    public class Board
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}