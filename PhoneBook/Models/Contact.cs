namespace PhoneBook.Models
{
    
    public class Contact
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public virtual UserProfile Owner { get; set; }
    }
}