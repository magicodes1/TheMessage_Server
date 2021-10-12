using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheMessage.Models
{
    public class Media
    {
        [Required]
        public int MediaId { get; set; }
        [Required]
        [Column(TypeName ="image")]
        public byte [] MediaContent { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}
