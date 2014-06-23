using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PhoneBook.Models;

namespace PhoneBookTest
{
    class TestingTime
    {
        [Test]
        public void SomeTest()
        {
            Contact contact = new Contact();
            contact.Name = "Mike Jones";
            Assert.AreEqual(contact.Name, "Mike Jones");// do your test in here
        }
    }
}
