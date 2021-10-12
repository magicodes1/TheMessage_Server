using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMessage.Models;
using TheMessage.Resources;

namespace TheMessage.Profies
{
    public class Mapping:Profile
    {
       
        public Mapping()
        {
            CreateMap<Media, MediaResource>().ReverseMap();

            CreateMap<Message, AddMessageResource>().ReverseMap();
            CreateMap<ApplicationUser, UserResource>().ReverseMap();

            CreateMap<MessageResource, Message>()
                .ForMember(dest => dest.User, src => src.MapFrom(x => x.User))
                .ForMember(dest => dest.Medias, src => src.MapFrom(x => x.Medias))
                .ReverseMap();



        }
    }
}
