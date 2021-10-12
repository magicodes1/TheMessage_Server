using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheMessage.Comunications;
using TheMessage.Interfaces.Repositories;
using TheMessage.Interfaces.Servives;
using TheMessage.Models;
using TheMessage.Resources;

namespace TheMessage.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        private async Task<List<byte[]>> addMedia(List<IFormFile> formFiles)
        {
            List<byte[]> medias = new List<byte[]>();

            if (formFiles != null)
            {
                foreach (var item in formFiles)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        byte[] media = stream.ToArray();
                        medias.Add(media);
                    }
                }
               
            }
            return medias;
        }


        public async Task<MessageResponse> add(AddMessageRequestResource addMessageRequestResource)
        {
            try
            {

                AddMessageResource addMessageResource = new AddMessageResource
                {
                    MessageContent = addMessageRequestResource.Message,
                    Time = addMessageRequestResource.Time,
                    UserId = addMessageRequestResource.Id
                };


                var formFiles = addMessageRequestResource.Images;

                var medias = formFiles != null ? await addMedia(formFiles) : null;

                if (medias != null && medias.Count > 0)
                {
                    List<MediaResource> mediaResources = new List<MediaResource>();

                    foreach (var item in medias)
                    {
                        mediaResources.Add(new MediaResource { MediaContent = item });
                    }
                    addMessageResource.Medias = mediaResources;
                }


               int messageId = await _messageRepository.add(addMessageResource);

                var messageContent = new MessageResource
                {
                    MessageId=messageId,
                    MessageContent = addMessageResource.MessageContent,
                    Time = addMessageResource.Time,
                    Medias = addMessageResource.Medias,
                    User = new UserResource { Id=addMessageRequestResource.Id,UserName=addMessageRequestResource.UserName,isOnline=addMessageRequestResource.IsOnline}
                };
                return new MessageResponse { Status = true, Message = "", MessageContent = messageContent };
            }
            catch (Exception ex)
            {

                return new MessageResponse { Status = false, Message = ex.Message};
            }
        }

        public async Task<AllMessageResponse> get()
        {
            var messages = await _messageRepository.get();
            return new AllMessageResponse { Status = true, Message = "", Messages = messages.ToList() };
        }
    }
}
