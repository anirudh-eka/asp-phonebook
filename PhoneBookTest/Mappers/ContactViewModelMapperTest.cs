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
        private ContactViewModelMapper mapper = new ContactViewModelMapper();

        [Test]
        public void CreatesNewViewModelGivenModel()
        {
            Contact model = new Contact(1, "Mike Jones", "281-330-8004");
            
            ContactViewModel result = mapper.Map(model);
            Assert.IsTrue(ModelAndViewModelAreEqual(model, result));
        }

        [Test]
        public void ShouldCreateListOfViewModelsGivenListOfModels()
        {
            List<Contact> contacts = new List<Contact>();
 
            contacts.Add(new Contact(1, "Mike Jones", "281-330-8004"));
            contacts.Add(new Contact(2, "Christine Jones", "281-330-8005"));

            List<ContactViewModel> results = mapper.MapList(contacts);

            Assert.IsTrue(ModelAndViewModelAreEqual(contacts[0], results[0]));
            Assert.IsTrue(ModelAndViewModelAreEqual(contacts[1], results[1]));
        }

        private bool ModelAndViewModelAreEqual(Contact model, ContactViewModel viewModel)
        {
            return model.Name == viewModel.Name && model.ID == viewModel.ID && model.Number == viewModel.Number;
        } 
    }
}
