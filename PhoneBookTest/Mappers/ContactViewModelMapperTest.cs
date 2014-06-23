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
    
    class ContactViewModelMapperTest
    {
        [Test]
        public void createsNewViewModelGivenModel()
        {
            Contact model = new Contact();
            model.ID = 1;
            model.Name = "Mike Jones";
            model.Number = "281-330-8004";

            ContactViewModel viewModel = new ContactViewModel();
            viewModel.ID = 1;
            viewModel.Name = "Mike Jones";
            viewModel.Number = "281-330-8004";


            ContactViewModelMapper mapper = new ContactViewModelMapper();
            ContactViewModel result = mapper.Map(model);
            Assert.AreEqual(model.Name, result.Name);
            Assert.AreEqual(model.Number, result.Number);
            Assert.AreEqual(model.ID, result.ID);// do your test in here
        }
    }
}
