using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheMessage.Resources
{
    public class MessageResource
    {
        public int MessageId { get; set; }
        [Required]
        public string MessageContent { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public UserResource User { get; set; }
        public List<MediaResource> Medias { get; set; }
    }
}
