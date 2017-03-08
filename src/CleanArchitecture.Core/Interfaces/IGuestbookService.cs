using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Interfaces
{
   public interface IGuestbookService
    {
        void RecordEntry(Guestbook guestbook, GuestbookEntry guestbookEntry);
    }
}
