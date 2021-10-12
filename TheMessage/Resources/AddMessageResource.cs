using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheMessage.Resources
{

    public class MediaResource
    {
        [Required]
        public byte[] MediaContent { get; set; }
    }
    

    public class AddMessageResource
    {
        [Required]
        public string MessageContent { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public string UserId { get; set; }
        public List<MediaResource> Medias { get; set; }
    }

   
    public class AddMessageRequestResource
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public bool IsOnline { get; set; }

        [BindProperty]
        [Display(Name ="images")]
        public List<IFormFile> Images { get; set; }
    }
}
