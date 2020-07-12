using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FrameworkConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World of MSBuild");
            Object[] ca = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(false);
            foreach (var item in ca)
            {
                if (item.GetType().FullName == "System.Runtime.Versioning.TargetFrameworkAttribute")
                {
                    System.Runtime.Versioning.TargetFrameworkAttribute t = (System.Runtime.Versioning.TargetFrameworkAttribute)item;
                    Console.WriteLine("Current .NET framework is: " + t.FrameworkDisplayName);
                }
            }
            Console.ReadLine();
        }
    }
}
