using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheMessage.Models
{
    public class Message
    {
        [Required]
        public int MessageId { get; set; }
        [Required]
        public string MessageContent { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

        public ICollection<Media> Medias { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
