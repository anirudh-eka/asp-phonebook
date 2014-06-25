using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PhoneBook.DAL;
using PhoneBook.Filters;
using PhoneBook.Mappers;
using PhoneBook.Models;
using PhoneBook.Queriers;
using PhoneBook.ViewModels;
using WebMatrix.WebData;

namespace PhoneBook.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ContactController : Controller
    {
        private PhoneBookContext db = new PhoneBookContext();
        private IMapToNew<Contact, ContactViewModel> contactViewModelMapper = new ContactViewModelMapper();
        private IMapToExisting<Contact, ContactViewModel> contactMapper = new ContactMapper();
        private ContactQuerier contactQuerier;
        private IMapToNewListMapper<Contact, ContactViewModel> contactViewModeListMapper; 

        public ContactController()
        {
            contactQuerier = new ContactQuerier(db);
            contactViewModeListMapper = new ContactViewModelListMapper(contactViewModelMapper);
        }
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            int currentUserId = WebSecurity.GetUserId(User.Identity.Name);
            IQueryable<Contact> contacts = contactQuerier.GetContactsFor(currentUserId);

            List<ContactViewModel> contactViewModels = contactViewModeListMapper.Map(contacts.ToList());
            
            return View(contactViewModels);
        }


        //
        // GET: /Contact/Details/5

        public ActionResult Details(int id = 0)
        {
            Contact contact = contactQuerier.GetContactById(id);
            int CurrentUserId = WebSecurity.GetUserId(User.Identity.Name);
            if (contact == null || (contact.Owner.UserId != CurrentUserId))
            {
                return HttpNotFound();
            }
            ContactViewModel contactViewModel = contactViewModelMapper.Map(contact);
            return View(contactViewModel);
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Contact/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                int CurrentUserId = WebSecurity.GetUserId(User.Identity.Name);
                UserProfile owner = db.UserProfiles.Find(CurrentUserId);
                
                Contact contact = new Contact();
                contactMapper.Map(contact, contactViewModel, owner);
                db.Contacts.Add(contact);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(contactViewModel);
        }

        //
        // GET: /Contact/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            ContactViewModel contactViewModel= contactViewModelMapper.Map(contact);
            return View(contactViewModel);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                Contact contact = db.Contacts.Find(contactViewModel.ID);
                contactMapper.Map(contact, contactViewModel);

                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactViewModel);
        }

        //
        // GET: /Contact/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Contacts/Search/Cameron

        public ActionResult Search(string id)
        {
            int currentUserId = WebSecurity.GetUserId(User.Identity.Name);

            IQueryable<Contact> contacts; 
            
            if (String.IsNullOrEmpty(id))
            {
                contacts = from contact in db.Contacts
                           where contact.Owner.UserId == currentUserId
                           select contact;
            }
            else
            {
                contacts = from contact in db.Contacts
                           where contact.Owner.UserId == currentUserId
                           && (contact.Name.Contains(id) || contact.Number.Contains(id))
                           select contact;
            }

            List<ContactViewModel> contactViewModels = contactViewModeListMapper.Map(contacts.ToList());
            return View("Index", contactViewModels);
        }

        // GET Contact/GetContacts
        public JsonResult GetContacts()
        {
            int currentUserId = WebSecurity.GetUserId(User.Identity.Name);
            string searchString = Request.Params.Get("searchString");
            var contacts = from contact in db.Contacts
                       where contact.Owner.UserId == currentUserId
                       && (contact.Name.Contains(searchString) || contact.Number.Contains(searchString))
                       select contact;

            List<ContactViewModel> contactViewModels = contactViewModeListMapper.Map(contacts.ToList());

            return Json(contactViewModels, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}