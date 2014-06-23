using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels
{
    public class ContactViewModel
    {
        public ContactViewModel()
        {
        }

        public ContactViewModel(int id, string name, string number)
        {
            this.ID = id;
            this.Name = name;
            this.Number = number;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }
}