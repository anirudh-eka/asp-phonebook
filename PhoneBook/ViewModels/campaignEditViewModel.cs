using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.ViewModels
{
    public class CampaignEditViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int ID { get; set; }
    }
}