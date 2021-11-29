using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RegExp
{
    class Regex
    {
        //(...) [...] . .-. | + * ? 
        List<Modul> modules = new List<Modul>();

        public Regex(string txt)
        {
            Parse(txt);
        }

        public static List<string> Match(string txt)
        {
            List<string> matches = new List<string>();

            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < txt.Length; i++)
            {
                if(txt.Substring(i, 4).Equals("+375"))
                {
                    sb.Append(txt.Substring(i, 4));
                    i += 4;
                    if(txt.Substring(i, 2).Equals("29")|| txt.Substring(i, 2).Equals("25")||
                        txt.Substring(i, 2).Equals("33")|| txt.Substring(i, 2).Equals("44"))
                    {
                        sb.Append(txt.Substring(i, 2));
                        i += 2;
                        int j;
                        if(Int32.TryParse(txt.Substring(i,7),out j))
                        {
                            sb.Append(txt.Substring(i, 7));
                            matches.Add(sb.ToString());
                            i += 7;
                        }
                    }
                }
                if (i+4 > txt.Length-1)
                    break;
                sb.Clear();
            }

            return matches;
        }

        private void Parse(string txt)
        {
            if (!Regex.valid(txt))
                return;

            StringBuilder sb = new StringBuilder();
            for(int i = 0; i<txt.Length;i++)
            {
                if(txt[i]=='('||txt[i]=='[' && sb.Length != 0)
                {
                    Modul modul = new Modul();
                    modul.modul = sb.ToString();
                    modul.type = SelectType(txt[i - 1]);
                    modules.Add(modul);
                    sb.Clear();
                }

                if (txt[i] == ')' || txt[i] == ']')
                {
                    Modul modul = new Modul();
                    modul.modul = sb.ToString();
                    char ch = ' ';
                    if (i != txt.Length - 1)
                        ch = txt[i + 1];
                    modul.type = SelectType(ch);
                    modules.Add(modul);
                    sb.Clear();
                }                               

                sb.Append(txt[i]);

                if (i == txt.Length - 1 && txt[i] != ')' && txt[i] != ']')
                {
                    Modul modul = new Modul();
                    modul.modul = sb.ToString();
                    modul.type = SelectType(txt[i]);
                    modules.Add(modul);
                    sb.Clear();
                }
            }
        }      

        public ModulType SelectType(char ch)
        {
            switch (ch)
            {
                case '+':
                    return ModulType.oneOrMore;
                case '*':
                    return ModulType.noneOrMore;
                case '?':
                    return ModulType.oneOrNone;
                default:
                    return ModulType.single;
            }
        }

        public static Boolean valid(string reg)
        {
            if (!CheckBracket(reg))
                return false;

            for(int i = 0; i < reg.Length; i++)
            {
                if (reg[i] == '\\' && i!=reg.Length-1)
                {
                    if (reg[i + 1] != 'n' || reg[i + 1] != 't')
                        return false;
                }
                if(reg[i] == '-' && i!=0 && i != reg.Length - 1)
                {
                    if (reg[i + 1] < reg[i - 1])
                        return false;
                }
            }


            return true;
        }

        public static Boolean CheckBracket(string txt)
        {
            Stack<char> chs = new Stack<char>();

            foreach(char ch in txt)
            {
                switch (ch)
                {
                    case '(':
                        chs.Push(ch);
                        break;
                    case '[':
                        chs.Push(ch);
                        break;
                    case '{':
                        chs.Push(ch);
                        break;
                    case ')':
                        if (chs.Peek() == '(')
                            chs.Pop();
                        else
                            return false;
                        break;
                    case ']':
                        if (chs.Peek() == '[')
                            chs.Pop();
                        else
                            return false;
                        break;
                    case '}':
                        if (chs.Peek() == '{')
                            chs.Pop();
                        else
                            return false;
                        break;
                }
            }
            if (chs.Count() == 0)
                return true;
            else
                return false;
        }
        
    }
}
