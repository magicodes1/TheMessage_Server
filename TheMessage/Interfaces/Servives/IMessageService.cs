using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheMessage.Comunications;
using TheMessage.Models;
using TheMessage.Resources;

namespace TheMessage.Interfaces.Servives
{
    public interface IMessageService
    {
        Task<MessageResponse> add(AddMessageRequestResource addMessageRequestResource);
        Task<AllMessageResponse> get();
    }
}
