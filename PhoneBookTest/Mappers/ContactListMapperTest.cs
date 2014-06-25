using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhoneBook.Mappers;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBookTest.Mappers
{
    class ContactListMapperTest
    {
        [Test]
        public void ShouldReturnListOfContactViewModelsGivenListOfContacts()
        {
            var mapperMock = new Mock<IMapToNew<Contact, ContactViewModel>>();

            var contactOne = new Contact(1, "Mike Jones", "281-330-8004");
            var contactTwo = new Contact(2, "Christine Jones", "281-330-8005");

            mapperMock.Setup(framework => framework.Map(contactOne))
                .Returns(new ContactViewModel(1, "Mike Jones", "281-330-8004"));
            mapperMock.Setup(framework => framework.Map(contactTwo))
                .Returns(new ContactViewModel(2, "Christine Jones", "281-330-8005"));

            IMapToNew<Contact, ContactViewModel> mapper = mapperMock.Object;

            IMapToNewListMapper<Contact, ContactViewModel> listMapper = new ContactViewModelListMapper(mapper);
            List<Contact> contacts = new List<Contact>();
            contacts.Add(contactOne);
            contacts.Add(contactTwo);

            List<ContactViewModel> results = listMapper.Map(contacts);

            Assert.IsTrue(ModelAndViewModelAreEqual(contacts[0], results[0]));
            Assert.IsTrue(ModelAndViewModelAreEqual(contacts[1], results[1]));
        }

        private bool ModelAndViewModelAreEqual(Contact model, ContactViewModel viewModel)
        {
            return model.Name == viewModel.Name && model.ID == viewModel.ID && model.Number == viewModel.Number;
        } 
    }
}
