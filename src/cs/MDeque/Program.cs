using System;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

namespace MDeque
{
    internal static partial class Program
    {
        private static void Main()
        {
            string? line;
            MDeque<double?> list = new MDeque<double?>();
            V8ScriptEngine engine = new V8ScriptEngine();

            engine.AddHostObject("list", list);
            engine.AddHostType(typeof(MDeque<double?>));

            do
            {
                Console.WriteLine(list);
                Console.WriteLine("First = {0}, Center = {1} (Even = {2}), Last = {3}", list.First, list.Center, list.Count % 2 == 0, list.Last);

                line = Console.ReadLine();

                if (line != null)
                {
                    try
                    {
                        Console.WriteLine(engine.Evaluate(line));
                    }
                    catch (ScriptEngineException scriptEngineException)
                    {
                        Console.WriteLine(scriptEngineException.Message);
                    }
                    catch (InvalidOperationException invalidOperationException)
                    {
                        Console.WriteLine(invalidOperationException.Message);
                    }
                }
            }
            while (line != null);
        }
    }
}
