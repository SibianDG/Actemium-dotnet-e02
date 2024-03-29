﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace _2021_dotnet_e_02.Models
{
    //[Keyless]
    public class ActemiumTicketChangeContent
    {
        public int TicketChangeContentId { get; set; }    

        public ActemiumTicketChange TicketChange { get; set; }

        public string ChangeContent { get; set; }

        public ActemiumTicketChangeContent()
        {
        }

        public ActemiumTicketChangeContent(ActemiumTicketChange ticketChange, string changeContent)
        {
            TicketChange = ticketChange;
            ChangeContent = changeContent;
        }
    }
}