using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PhoneBook.Mappers;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBookTest.Mappers
{
    class ContactMapperTest
    {
        [Test]
        public void ShouldMapChangesFromViewModelToModel()
        {
            ContactMapper contactMapper = new ContactMapper();

            UserProfile owner = new UserProfile();
            Contact model = new Contact();
            model.ID = 1;
            model.Name = "Mike Jones";
            model.Number = "912-457-9800";
            model.Owner = owner;

            ContactViewModel viewModel = new ContactViewModel();
            viewModel.ID = 1;
            viewModel.Name = "Mikey Jones";
            viewModel.Number = "912-457-9800";

            contactMapper.Map(model, viewModel);
            Assert.IsTrue(ModelAndViewModelAreEqual(model, viewModel, owner));
        }

        [Test]
        public void ShouldMapChangesFromViewModelWithOwnerToModel()
        {
            ContactMapper contactMapper = new ContactMapper();

            ContactViewModel viewModel = new ContactViewModel();
            viewModel.ID = 1;
            viewModel.Name = "Mikey Jones";
            viewModel.Number = "912-457-9800";
            UserProfile owner = new UserProfile();

            Contact model = new Contact();

            contactMapper.Map(model, viewModel, owner);
            Assert.IsTrue(ModelAndViewModelAreEqual(model, viewModel, owner));
        }

        private bool ModelAndViewModelAreEqual(Contact model, ContactViewModel viewModel, UserProfile correctOwner)
        {
            return model.Name == viewModel.Name && model.ID == viewModel.ID && 
                model.Number == viewModel.Number && model.Owner == correctOwner;
        } 

    }
}
