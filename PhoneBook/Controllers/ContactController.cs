using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook.DAL;
using PhoneBook.Filters;
using PhoneBook.Mappers;
using PhoneBook.Models;
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
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            IQueryable<Contact> contacts = from contact in db.Contacts select contact;
            
            int CurrentUserId = WebSecurity.GetUserId(User.Identity.Name);
            contacts = contacts.Where(c => c.Owner.UserId == CurrentUserId);

            List<ContactViewModel> contactViewModels = new List<ContactViewModel>();
            foreach (Contact contact in contacts)
            {
                contactViewModels.Add(contactViewModelMapper.Map(contact));
            }
            return View(contactViewModels);
        }

        //
        // GET: /Contact/Details/5

        public ActionResult Details(int id = 0)
        {
            Contact contact = db.Contacts.Find(id);
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
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                int CurrentUserId = WebSecurity.GetUserId(User.Identity.Name);
                UserProfile owner = db.UserProfiles.Find(CurrentUserId);

                if (owner != null)
                {
                    contact.Owner = owner;
                    db.Contacts.Add(contact);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(contact);
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
            int CurrentUserId = WebSecurity.GetUserId(User.Identity.Name);
            UserProfile owner = db.UserProfiles.Find(CurrentUserId);

            IQueryable<Contact> contacts = Enumerable.Empty<Contact>().AsQueryable();

            
            if (String.IsNullOrEmpty(id))
            {
                contacts = from contact in db.Contacts
                           where contact.Owner.UserId == CurrentUserId
                           select contact;
            }
            else
            {
                contacts = from contact in db.Contacts
                           where contact.Owner.UserId == CurrentUserId
                           && (contact.Name.Contains(id) || contact.Number.Contains(id))
                           select contact;
            }

            return View("Index", contacts);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}