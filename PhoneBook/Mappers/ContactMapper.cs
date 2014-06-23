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
        public void Map(Contact model, ContactViewModel viewModel, UserProfile owner)
        {
            model.Name = viewModel.Name;
            model.ID = viewModel.ID;
            model.Number = viewModel.Number;
            if (owner != null)
            {
                model.Owner = owner;
            }
        }

        public void Map(Contact source, ContactViewModel target)
        {
            Map(source, target, null);
        }
    }
}