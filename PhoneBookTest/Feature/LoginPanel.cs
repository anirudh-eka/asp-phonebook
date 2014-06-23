using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Locators;

namespace PhoneBookTest.Feature
{
    public class LoginPanel : UiComponent
    {
        public bool IsLoggedIn
        {
            get { return Find.OptionalElement(By.Id("login-panel")) == null; }
        }

        public string LoggedInUserName
        {
            get { return Find.Element(By.Id("login-username")).Text; }
        }
    }
}
