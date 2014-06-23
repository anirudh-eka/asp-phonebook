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
        public void shouldMapChangesFromViewModelToModel()
        {
            ContactMapper contactMapper = new ContactMapper();

            Contact model = new Contact();
            model.ID = 1;
            model.Name = "Mike Jones";
            model.Number = "912-457-9800";

            ContactViewModel viewModel = new ContactViewModel();
            viewModel.ID = 1;
            viewModel.Name = "Mikey Jones";
            viewModel.Number = "912-457-9800";

            contactMapper.Map(model, viewModel);
            Assert.AreEqual(model.Name, viewModel.Name);
        }
    }
}
