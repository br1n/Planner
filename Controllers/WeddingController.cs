using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace WeddingPlanner.Controllers
{
    [Route("wedding")]
    public class WeddingController : Controller
    {
        public MainUser ActiveUser
        //produces ActiveUser object that matches that of SessionUser
        {
            get { return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("user_Id")); }
        }

        private MyContext dbContext;
        public WeddingController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult Index()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            ViewBag.User = ActiveUser;
            ViewBag.Weddings = dbContext.Weddings
            .Include(w => w.Attendees)
            .ThenInclude(a => a.attendingGuests);
            // .OrderByDescending(w => w.CreatedAt);
            return View("Index");
        }

        [HttpGet("{weddingId}")]
        public IActionResult WeddingInfo(int weddingId)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            var wedding_Info = dbContext.Weddings
            .Include(w => w.Attendees)
            .ThenInclude(a => a.attendingGuests)
            .FirstOrDefault(a => a.WeddingId == weddingId);
            ViewBag.Wedding = wedding_Info;
            return View();
        }

        [HttpGet("new_wedding")]
        public IActionResult NewWedding()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            var myUser = ActiveUser;
            ViewBag.User = myUser;
            return View();
        }

        [HttpPost("create_wedding")]
        public IActionResult CreateWedding(Wedding newWedding)
        {
            // MainUser weddingUser = ActiveUser;

            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            if(ModelState.IsValid)
            {
                dbContext.Weddings.Add(newWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User = ActiveUser;
            return View("NewWedding");
        }

        [HttpGet("rsvp/{weddingID}")]
        public IActionResult AttendWedding(int weddingID)
        {
            Attending attending = new Attending
            {
                UserId = ActiveUser.UserId,
                WeddingId = weddingID
            };
            dbContext.Attendees.Add(attending);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("unrsvp/{weddingID}")]
        public IActionResult UnattendWedding(int weddingID)
        {
            int? UserId = ActiveUser.UserId;
            int? WeddingId = weddingID;
            Attending unattending = dbContext.Attendees
            .FirstOrDefault(a => a.WeddingId == weddingID && a.UserId == UserId);
            dbContext.Attendees.Remove(unattending);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{WeddingID}")]
        public IActionResult DeleteWedding(int WeddingID)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            Wedding deleteWedding = dbContext.Weddings
            .FirstOrDefault(d => d.WeddingId == WeddingID);
            dbContext.Weddings.Remove(deleteWedding);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}