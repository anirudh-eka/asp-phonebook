using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno.PageObjects;

namespace PhoneBookTest.Feature
{
    public class HomePage : Page
    {
        public LoginPanel LoginPanel
        {
            get { return GetComponent<LoginPanel>(); }
        }
    }
}
