using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class UserProfilesController : Controller
    {
        private ketotrackerEntities db = new ketotrackerEntities();

        private string _currentUserId;
        public string CurrentUserId
        {
            get
            {
                if (_currentUserId == null)
                {
                    _currentUserId = User.Identity.GetUserId();
                }
                return _currentUserId;
            }
            set
            {
                _currentUserId = value;
            }
        }


        // GET: UserProfiles
        public ActionResult Index()
        {

            UserProfile userProfile = db.UserProfiles.FirstOrDefault(x => x.AspNetUserId == CurrentUserId);
            if (userProfile == null)
            {
                userProfile = new UserProfile();
                userProfile.AspNetUserId = CurrentUserId;
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();
            }
            return View(userProfile);
        }


        // GET: UserProfiles/Edit/5
        public ActionResult Edit()
        {

            UserProfile userProfile = db.UserProfiles.FirstOrDefault(x => x.AspNetUserId == CurrentUserId);
            if (CurrentUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (userProfile == null)
            {
                userProfile = new UserProfile();
                userProfile.AspNetUserId = CurrentUserId;
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();
            }
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserProfileId,AspNetUserId,CaloricIntakePerDay,FatPerDay,ProteinPerDay,NetCarbsPerDay")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userProfile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}




