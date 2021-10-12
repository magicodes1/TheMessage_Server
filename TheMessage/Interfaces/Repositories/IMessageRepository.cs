using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMessage.Comunications;
using TheMessage.Resources;

namespace TheMessage.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<MessageResource>> get();
        Task<int> add(AddMessageResource addMessageResource);
        
    }
}
