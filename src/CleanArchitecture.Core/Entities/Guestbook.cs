using CleanArchitecture.Core.Events;
using CleanArchitecture.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class Guestbook : BaseEntity
    {
        public string Name { get; set; }
        public List<GuestbookEntry> Entries { get; } = new List<GuestbookEntry>();
        public void AddEntry(GuestbookEntry entry)
        {
            var addedEvent = new EntryAddEvent(entry,Id);
            Entries.Add(entry);
            Events.Add(addedEvent);
        }
    }
}
