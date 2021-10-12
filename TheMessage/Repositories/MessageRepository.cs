using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMessage.Comunications;
using TheMessage.Data;
using TheMessage.Interfaces.Repositories;
using TheMessage.Models;
using TheMessage.Resources;

namespace TheMessage.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(ChatDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> add(AddMessageResource addMessageResource)
        {
            var message = _mapper.Map<Message>(addMessageResource);

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message.MessageId;
        }

        public async Task<IEnumerable<MessageResource>> get()
        {
            List<MessageResource> messages = await _context.Messages.AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Medias)
                .OrderBy(p => p.Time.Millisecond)
                .Select(p => _mapper.Map<MessageResource>(p))
                .ToListAsync();
                

            return messages;
        }
    }
}
