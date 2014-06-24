using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Mappers
{
    public class ContactViewModelMapper : IMapToNew<Contact, ContactViewModel>
    {
        public ContactViewModel Map(Contact contact)
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.Name = contact.Name;
            contactViewModel.Number = contact.Number;
            contactViewModel.ID = contact.ID;

            return contactViewModel;
        }

        public List<ContactViewModel> MapList(List<Contact> contacts)
        {
            List<ContactViewModel> contactViewModels = new List<ContactViewModel>();
            foreach (Contact contact in contacts)
            {
                contactViewModels.Add(Map(contact));
            }

            return contactViewModels;
        }
    }
}