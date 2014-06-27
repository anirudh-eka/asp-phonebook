using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels
{
    public class CampaignExportModel
    {
        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDateTime { get; set; }
    }
}