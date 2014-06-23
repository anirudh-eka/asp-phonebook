using System.Runtime.InteropServices;

namespace PhoneBook.Models
{
    
    public class Contact
    {
        public Contact()
        {
        }

        public Contact(int id, string name, string number)
        {
            this.ID = id;
            this.Name = name;
            this.Number = number;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public virtual UserProfile Owner { get; set; }
    }
}