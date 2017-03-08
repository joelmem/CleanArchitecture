using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Events
{
   public class EntryAddEvent : BaseDomainEvent
    {
        public int GuestbookId { get; set; }
        public GuestbookEntry Entry { get; set; }

        public EntryAddEvent(GuestbookEntry entry, int guestbookId)
        {
            Entry = entry;
            GuestbookId = guestbookId;
        }
    }
}
