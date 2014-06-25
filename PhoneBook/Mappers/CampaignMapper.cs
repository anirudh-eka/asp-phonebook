using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Mappers
{
    public class CampaignMapper : IMapToExisting<CampaignViewModel, Campaign>
    {
        public void Map(CampaignViewModel source, Campaign target)
        {
            target.Name = source.Name;
            target.Date = source.Date;
        }
    }
}