using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Mappers
{
    public interface IMapToNewListMapper<TSource, TTarget>
    {
        List<TTarget> Map(List<TSource> contacts);
    }
}