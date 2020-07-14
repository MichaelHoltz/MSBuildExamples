using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkConsoleApp
{
    public class DemoClass
    {
        private String name;
        public string Name { get => name; set => name = value; }

        public int StringToInt(String input){
            if (int.TryParse(input, out int result))
                Console.WriteLine("StringToInt:               " + result);
            else
                Console.WriteLine("Could not parse input");
            return result;
        }
        public System.Collections.Generic.IEnumerable<int> Range(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                yield return i;
            }
            yield break;
        }
        public void YieldStatement()
        {
            foreach (int i in Range(-10, 10))
            {
                Console.WriteLine(i);
                if (i > 0)
                    break;
            }
        }
#if NETCOREAPP3_1
        public String GetNameGreeting(String fn, String mn, String ln)
        {
            return (fn, mn, ln) switch
            {
                (string f, string m, string l) => $"{f} {m[0]}. {l}",
                (string f, null, string l) => $"{f} {l}",
                (string f, string m, null) => $"{f} {m[0]}",
                (string f, null, null) => $"{f}",
                (null, string m, string l) => $"Ms/Mr {m[0]}. {l}",
                (null, null, string l) => $"Ms/Mr {l}",
                (null, string m, null) => $"Ms/Mr {m}",
                (null, null, null) => $"Someone"
            };
        }
#endif
    }
}
