using System;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

namespace MDeque
{
    internal static partial class Program
    {
        private static void Main()
        {
#if DEBUG
            string? line;
            MDeque<double?> list = new MDeque<double?>();
            MDeque<double?> list2 = new MDeque<double?>();
            V8ScriptEngine engine = new V8ScriptEngine();

            engine.AddHostObject("list", list);
            engine.AddHostObject("list2", list2);

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
#else
            Console.WriteLine(SequencePrompt);

            string? sequence = Console.ReadLine();

            if (sequence != null)
            {
                Console.WriteLine(InstructionsPrompt);

                string? instructions = Console.ReadLine();

                if (instructions != null)
                {
                    if (!Decoder.IsValid(instructions))
                    {
                        Console.WriteLine(InvalidInstructionsMessage);
                    }

                    MDeque<int> list = Decoder.Parse(sequence);

                    try
                    {
                        Decoder.Decode(list, instructions);
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine(EmptySequenceMessage);
                    }

                    Console.WriteLine(OutputMessage);
                    Console.WriteLine(list);
                }
            }
#endif
        }
    }
}
