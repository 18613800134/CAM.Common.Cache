using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Common.Cache
{
    public interface ICache
    {
        void add(string Key, object Value);
        object get(string Key);
        void delete(string Key);
    }
}
