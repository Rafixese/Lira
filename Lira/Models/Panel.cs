using System;
using System.ComponentModel.DataAnnotations;

namespace Lira.Models
{
    public class Panel
    {
        [Key] public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BoardId { get; set; }
        public Board Board { get; set; }
    }
}