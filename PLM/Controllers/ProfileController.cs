﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PLM;
using Microsoft.AspNet.Identity;

namespace PLM.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.UserID = User.Identity.Name;
            return View(db.Modules.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile()
        //[Bind(Include = "PictureID,Location,AnswerID")] Picture picture
        {
            if (ModelState.IsValid)
            {
                var name = User.Identity.GetUserName();
                ApplicationUser currentUser = (ApplicationUser)db.Users.Single(x => x.UserName == name);
                var location = SaveUploadedFileProfile(currentUser.Id);

                if (location == "")
                {
                    //error
                }
                else
                {
                  currentUser.ProfilePicture = location;
                }

                db.SaveChanges(); 
            }

            return RedirectToAction("Index");
        }

        public string SaveUploadedFileProfile(string UserId)
        {
            Session["upload"] = UserId;
            bool isSavedSuccessfully = true;
            string fName = "";
            string path = "";
            string relpath = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        string moduleDirectory = (Path.Combine(Server.MapPath("~/Content/Images/PLM/" + Session["upload"].ToString() + "/")));
                        if (!Directory.Exists(moduleDirectory))
                        {
                            Directory.CreateDirectory(moduleDirectory);
                        }
                        path = moduleDirectory + fName;
                        relpath = ("/Content/Images/PLM/" + Session["upload"].ToString() + "/" + fName);
                        file.SaveAs(path);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
            {
                return relpath;
            }
            else
            {
                return "FAILED";
            }
        }
    }
}