﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMessage.Resources;

namespace TheMessage.Comunications
{
    public class OnlineUserResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<UserResource> Users { get; set; }
    }
}
