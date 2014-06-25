using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Campaign
    {
        public int ID { get; set; }
    
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public virtual ICollection<Contact> Contact { get; set; }
    }
}