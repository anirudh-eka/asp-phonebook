using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Mappers
{
    public class ContactMapper : IMapToExisting<Contact, ContactViewModel>
    {
        public void Map(Contact model, ContactViewModel viewModel)
        {
            model.Name = viewModel.Name;
            model.ID = viewModel.ID;
            model.Number = viewModel.Number;
        }
    }
}