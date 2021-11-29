using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegExp
{    
    public class Modul
    {
        public string modul;        
               
        public ModulType type;
    }

    public enum ModulType
    {
        single,
        oneOrMore,
        noneOrMore,
        oneOrNone,

    }
}
