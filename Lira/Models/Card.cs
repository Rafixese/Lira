using System;
using System.ComponentModel.DataAnnotations;

namespace Lira.Models
{
    public class Card
    {
        [Key] public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid PanelId { get; set; }
        public Panel Panel { get; set; }
    }
}