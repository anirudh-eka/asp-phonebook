using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Mappers
{
    public class CampaignEditViewModelMapper : IMapToExisting<Campaign, CampaignEditViewModel >
    {
        public void Map(Campaign source, CampaignEditViewModel target)
        {
            target.Name = source.Name;
            target.Date = source.Date;
            target.ID = source.ID;


            IMapToNewListMapper<Contact, ContactViewModel> contactListMapper = 
                new ContactViewModelListMapper(new ContactViewModelMapper());
           
            target.Contacts = contactListMapper.Map(source.Contacts.ToList());
        }
    }
}