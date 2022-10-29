using System;

namespace MDeque
{
    internal static partial class Program
    {
        private static void Main()
        {
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
                        Console.WriteLine(ErrorMessage);
                    }

                    MDeque<int>? list = Decoder.Parse(sequence);

                    Decoder.Decode(list, instructions);

                    Console.WriteLine(OutputMessage);
                    Console.WriteLine(list);
                }
            }
        }
    }
}
