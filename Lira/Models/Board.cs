using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lira.Models
{
    public class Board
    {
        public Guid BoardId { get; set; }
        public string Name { get; set; }
        [Key]
        [ForeignKey("AspNetUsers")]
        public Guid UserId { get; set; }
    }
}