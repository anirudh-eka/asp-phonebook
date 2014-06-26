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
                return RedirectToAction("Edit/" + campaign.ID);
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
        public JsonResult PostContact(int contactID, int campaignID)
        {
            var contact = db.Contacts.Find(contactID);
            var campaign = db.Campaigns.Find(campaignID);

            campaign.Contact.Add(contact);
            db.SaveChanges();
            
            return Json("Sucess");
        }
        
    }
}