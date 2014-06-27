using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PhoneBook.Filters;
using PhoneBook.Mappers;
using PhoneBook.Models;
using PhoneBook.DAL;
using PhoneBook.ViewModels;

namespace PhoneBook.Controllers
{

    [Authorize]
    public class CampaignController : Controller
    {
        private PhoneBookContext db = new PhoneBookContext();
        private IMapToExisting<CampaignViewModel, Campaign> campaignMapper = new CampaignMapper();
        private IMapToExisting<Campaign, CampaignEditViewModel> campaignEditViewModelMapper =
            new CampaignEditViewModelMapper();
        private IMapToExisting<CampaignEditViewModel, Campaign> campaignFromEditViewModelMapper = new CampaignFromEditViewModelMapper();
        //
        // GET: /Campaign/
        public ActionResult Index()
        {
            return View(db.Campaigns.ToList());
        }

        //
        // GET: /Campaign/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Campaign/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CampaignViewModel campaignViewModel)
        {
            if (ModelState.IsValid)
            {
                Campaign campaign = new Campaign();
                campaignMapper.Map(campaignViewModel, campaign);
                db.Campaigns.Add(campaign);
                db.SaveChanges();
                return RedirectToAction("AddContacts/" + campaign.ID);
            }

            return View(campaignViewModel);
        }

        //
        // GET: /Campaign/Edit/5

        public ActionResult Edit(int id = 0)
        {   
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            CampaignEditViewModel campaignEditViewModel = new CampaignEditViewModel();
            campaignEditViewModelMapper.Map(campaign, campaignEditViewModel);
            return View(campaignEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CampaignEditViewModel campaignEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var campaign = db.Campaigns.Find(campaignEditViewModel.ID);
                campaignFromEditViewModelMapper.Map(campaignEditViewModel, campaign);

                db.Entry(campaign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campaignEditViewModel);
        }

        public ActionResult AddContacts(int id = 0)
        {
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            CampaignEditViewModel campaignEditViewModel = new CampaignEditViewModel();
            campaignEditViewModelMapper.Map(campaign, campaignEditViewModel);
            return View(campaignEditViewModel);
        }

        [HttpPost]
        public JsonResult PostContact(int contactID, int campaignID)
        {
            var contact = db.Contacts.Find(contactID);
            var campaign = db.Campaigns.Find(campaignID);

            campaign.Contacts.Add(contact);
            db.SaveChanges();
            
            return Json("Success");
        }

        //Post: /campaign/deleteContact
        [HttpPost]
        public JsonResult DeleteContact(int contactID, int campaignID)
        {
            var campaign = db.Campaigns.Find(campaignID);
            var contact = db.Contacts.Find(contactID);
            campaign.Contacts.Remove(contact);

            db.SaveChanges();
            return Json("Success");
        }

        //GET: /campaign/Export
        public ActionResult Export()
        {
            return View();
        }


        [HttpPost, ActionName("Export")]
        public ActionResult Export(CampaignExportModel campaignExportModel)
        {
            DateTime startDateTime = campaignExportModel.StartDateTime;
            DateTime endDateTime = campaignExportModel.EndDateTime;

            IQueryable contactWithCampaigns =
                from contacts in db.Contacts
                from campaign in contacts.Campaign
                where campaign.Date >= startDateTime && campaign.Date <= endDateTime
                select new ContactWithCampaign()
                {
                    Contact = contacts,
                    Campaign = campaign
                };

            string path = @"c:\exportedCampaigns\exportedCampaign.txt";

            // This text is always added, making the file longer over time
            // if it is not deleted.
            string headers = "Campaign Name, Campaign Date, Contact Name, Contact Phone Number" + Environment.NewLine;
            System.IO.File.WriteAllText(path, headers);
            foreach (ContactWithCampaign contactWithCampaign in contactWithCampaigns)
            {
                System.IO.File.AppendAllText(path,
                    contactWithCampaign.Campaign.Name + "," + contactWithCampaign.Campaign.Date + "," + contactWithCampaign.Contact.Name + "," + contactWithCampaign.Contact.Number + Environment.NewLine);
            }

            ViewBag.exportStatus = "Export successful";
            return View("Index", db.Campaigns.ToList());
        }
    }
}