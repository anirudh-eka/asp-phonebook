using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Mappers
{
    public class ContactViewModelListMapper :  IMapToNewListMapper<Contact, ContactViewModel>
    {
        private IMapToNew<Contact, ContactViewModel> Mapper;
        public ContactViewModelListMapper(IMapToNew<Contact, ContactViewModel> mapper)
        {
            this.Mapper = mapper;
        }
        public List<ContactViewModel> Map(List<Contact> contacts)
        {
            List<ContactViewModel> contactViewModels = new List<ContactViewModel>();
            foreach (Contact contact in contacts)
            {
                contactViewModels.Add(Mapper.Map(contact));
            }

            return contactViewModels;
        }
    }
}