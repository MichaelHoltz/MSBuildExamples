using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;

namespace FrameworkConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Hello World of MSBuild!");
            Console.ForegroundColor = c;
            Console.WriteLine(""); //Blank Line

            PrintTargetFramework();

            PrintBuildType();

            PrintBambooInfo();

            DemoClass dc = new DemoClass();
            dc.Name = "Demo Class";
            string json = JsonConvert.SerializeObject(dc);
            Assembly jc = typeof(JsonConvert).Assembly;
            Console.WriteLine("Newtonsoft.Json:           " + jc.FullName);


            Console.WriteLine(""); //Blank Line
            //Keep the console open to be able to see what it says if not run from a Command Window
            Console.WriteLine("Press Any Key to continue");
            Console.ReadLine();
        }
        static void PrintTargetFramework()
        {
            Object[] ca = Assembly.GetEntryAssembly().GetCustomAttributes(false);
            foreach (var item in ca)
            {
                if (item.GetType().FullName == "System.Runtime.Versioning.TargetFrameworkAttribute")
                {
                    System.Runtime.Versioning.TargetFrameworkAttribute t = (System.Runtime.Versioning.TargetFrameworkAttribute)item;
                    Console.WriteLine("Current .NET Framework is: " + t.FrameworkDisplayName);
                }
            }
        }
        static void PrintBuildType()
        {
            Assembly ReflectedAssembly = typeof(Program).Assembly;

            object[] attribs = ReflectedAssembly.GetCustomAttributes(typeof(DebuggableAttribute), false);
            bool IsJITOptimized = false;
            //bool HasDebuggableAttribute = false;
            String DebugOutput = "";
            String BuildType = "Unknown";
            // If the 'DebuggableAttribute' is not found then it is definitely an OPTIMIZED build
            if (attribs.Length > 0)
            {
                // Just because the 'DebuggableAttribute' is found doesn't necessarily mean
                // it's a DEBUG build; we have to check the JIT Optimization flag
                // i.e. it could have the "generate PDB" checked but have JIT Optimization enabled
                DebuggableAttribute debuggableAttribute = attribs[0] as DebuggableAttribute;
                if (debuggableAttribute != null)
                {
                    //HasDebuggableAttribute = true;
                    IsJITOptimized = !debuggableAttribute.IsJITOptimizerDisabled;
                    BuildType = debuggableAttribute.IsJITOptimizerDisabled ? "Debug" : "Release";

                    // check for Debug Output "full" or "pdb-only"
                    DebugOutput = (debuggableAttribute.DebuggingFlags &
                                    DebuggableAttribute.DebuggingModes.Default) !=
                                    DebuggableAttribute.DebuggingModes.None
                                    ? " Full" : " pdb-only";
                }
            }
            else
            {
                IsJITOptimized = true;
                BuildType = "Release";
            }

            Console.WriteLine("BuildType:                 " + BuildType + DebugOutput);
        }

        static void PrintBambooInfo()
        {
            if (File.Exists("BambooInfo.txt"))
            {
                String bambooInfo = File.ReadAllText("BambooInfo.txt");
                Console.WriteLine("Bamboo Build Number:       " + bambooInfo);
            }
        }
    }
}
