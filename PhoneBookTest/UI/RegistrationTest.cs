using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PhoneBookTest.Feature
{
    class RegistrationTests
    {
        [Test]
        public void GivenAUserIsntRegistered_WhenRegisteringThem_TheyEndUpBackOnTheHomepageAndLoggedIn()
        {
            var page = Host.Instance.NavigateToInitialPage<HomePage>();

            Assert.That(page.Title, Is.EqualTo("Log in - Phone Book"));
        }
    }
}
