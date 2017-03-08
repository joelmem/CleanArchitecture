using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web.ViewModels;
using CleanArchitecture.Core.Interfaces;

namespace CleanArchitecture.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Guestbook> _guestbookRepository;
        private readonly IMessageSender _messageSender;
        private readonly IGuestbookService _guestbookService;
        public HomeController(IRepository<Guestbook> guestbookRepository, IMessageSender messageSender, IGuestbookService guestbookService )
        {
            _guestbookRepository = guestbookRepository;
            _messageSender = messageSender;
            _guestbookService = guestbookService;
        }
        [HttpPost]
        public IActionResult Index(HomePageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var guestbook = _guestbookRepository.GetById(1);
                guestbook.AddEntry(model.NewEntry);
                _guestbookRepository.Update(guestbook);        
                model.PreviousEntries.Clear();
                model.PreviousEntries.AddRange(guestbook.Entries);
            }
            return View(model);
        }
        public IActionResult Index()
        {
            if (!_guestbookRepository.List().Any())
            {
                var newGuestbook = new Guestbook() { Name = "My Guestbook", Id =1};
                newGuestbook.Entries.Add(new GuestbookEntry()
                {
                    EmailAddress = "steve@deviq.com",
                    Message = "Hi"
                });
                _guestbookRepository.Add(newGuestbook);        
                
            }

            //var guestbook = new Guestbook { Name = "My Guestbook"};
            var guestbook = _guestbookRepository.GetById(1);
            //guestbook.Entries.Add(new GuestbookEntry { Id = 1, Message = "Hello", EmailAddress = "Qa@mem.com", DateTimeCreated = DateTime.UtcNow.AddHours(-2) });
            //guestbook.Entries.Add(new GuestbookEntry { Id = 2, Message = "Bye", EmailAddress = "Perry@mem.com", DateTimeCreated = DateTime.UtcNow.AddHours(-1) });
            //guestbook.Entries.Add(new GuestbookEntry { Id = 3, Message = "Good", EmailAddress = "Nate@mem.com" });

            var viewModel = new HomePageViewModel();
            viewModel.GuestbookName = guestbook.Name;
            viewModel.PreviousEntries.AddRange(guestbook.Entries);
            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
