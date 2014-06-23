using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Mappers
{
    public interface IMapToNew<TSource, TTarget>
    {
        TTarget Map(TSource source);

        List<TTarget> MapList(List<TSource> sources);
    }
}
