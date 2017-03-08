using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Core.Entities;
using System.Linq;

namespace CleanArchitecture.Core.Services
{
    public class GuestbookService : IGuestbookService

    {
        private readonly IRepository<Guestbook> _guestbookRepository;
        private readonly IMessageSender _messageSender;
        public GuestbookService(IRepository <Guestbook> guestbookRepository, IMessageSender messageSender)
        {
            _guestbookRepository = guestbookRepository;
            _messageSender = messageSender;
        }
        public void RecordEntry(Guestbook guestbook, GuestbookEntry guestbookEntry)
        {
         
        }
    }
}
