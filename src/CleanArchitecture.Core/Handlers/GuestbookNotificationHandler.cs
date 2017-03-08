using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Events;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArchitecture.Core.Handlers
{
    public class GuestbookNotificationHandler : IHandle<EntryAddEvent>
    {
        private readonly IRepository<Guestbook> _guestbookRepository;
        private readonly IMessageSender _messageSender;
        public GuestbookNotificationHandler(IRepository<Guestbook> guestbookRepository, IMessageSender messageSender)
        {
            _guestbookRepository = guestbookRepository;
            _messageSender = messageSender;
        }
        public void Handle(EntryAddEvent domainEvent)
        {
            var guestbook = _guestbookRepository.GetById(domainEvent.GuestbookId);
            foreach (var entry in guestbook.Entries.Where(e => e.DateTimeCreated > DateTime.UtcNow.AddDays(-1)).ToList())
            {
                _messageSender.SendGuestbookNotificationEmail(entry.EmailAddress, "Guestbook signed: " + domainEvent.Entry.Message);
            }
        }
    }
}
