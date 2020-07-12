using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using Newtonsoft.json;

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
            if(File.Exists("BambooInfo.txt")){
                String bambooInfo = File.ReadAllText("BambooInfo.txt");
                Console.WriteLine("Bamboo Build Number = " + bambooInfo);
            }

            

            Console.WriteLine("Press Any Key to continue");
            //Keep the console open to be able to see what it says if not run from a Command Window
            Console.ReadLine();
        }
    }
}
