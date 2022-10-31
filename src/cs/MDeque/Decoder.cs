using System;

namespace MDeque
{
    /// <summary>
    /// Provides methods for processing a sequence of numbers according to given instructions. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// <para>The sequence of values provided by the user is a comma- and space-separated list of integers. For example: 12, 43, 189, 42, 1, 35.</para>
    /// The instructions consist of three characters:
    /// <list type="table">
    ///   <item>
    ///     <term>F</term>
    ///     <description>drop the first element of the sequence</description>
    ///   </item>
    ///   <item>
    ///     <term>B</term>
    ///     <description>drop the last element of the sequence</description>
    ///   </item>
    ///   <item>
    ///     <term>R</term>
    ///     <description>reverse the sequence</description>
    ///   </item>
    /// </list>
    /// <para>The program outputs the resulting numerical sequence after the instructions were processed.</para>
    /// <para>For example, if the initial sequence is 12, 43, 189, 42, 1, 35 and the instructions are "FRB", the resulting sequence should be 35, 1, 42, 189.</para>
    /// </remarks>
    public static class Decoder
    {
        /// <summary>
        /// Decode the sequence represented by the <paramref name="list"/> m-deque following the <paramref name="instructions"/>.
        /// </summary>
        /// <param name="list">The m-deque with the sequence to decode.</param>
        /// <param name="instructions">Instructions to follow to decode the <param name="list"/>.</param>
        /// <exception cref="InvalidOperationException">The sequence is empty and the next instruction is either 'F' or 'B'.</exception>
        public static void Decode(MDeque<int> list, string instructions)
        {
            foreach (char instruction in instructions)
            {
                switch (instruction)
                {
                    case 'F':
                        if (list.Count == 0)
                        {
                            throw new InvalidOperationException();
                        }

                        list.RemoveFirst();
                        break;

                    case 'B':
                        if (list.Count == 0)
                        {
                            throw new InvalidOperationException();
                        }

                        list.RemoveLast();
                        break;

                    case 'R':
                        for (int i = 0; i < list.Count; i++)
                        {
                            list.AddFirst(list.RemoveLast());
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Converts a given sequence from string format to m-deque of integer values. 
        /// </summary>
        /// <param name="sequence">A string with comma- and space-separated values.</param>
        /// <exception cref="ArgumentException">The sequence contains values that cannot be converted to a list of integer due to invalid characters or invalid separators.</exception>
        /// <returns>An m-deque with the same values as the ones listed in the <paramref name="sequence"/>.</returns>
        public static MDeque<int> Parse(string sequence)
        {
            MDeque<int> list = new MDeque<int>();
            string[] splitSequence = sequence.Split(separator: ", ");

            for (int i = 0; i < splitSequence.Length; i++)
            {
                list.AddLast(int.Parse(splitSequence[i]));
            }

            return list;
        }

        /// <summary>
        /// Determines if the sequence of instructions is valid. A valid sequence consists of characters: 'R', 'F' and 'B' in any order. 
        /// </summary>
        /// <param name="instructions">The instruction string.</param>
        /// <returns><see langword="true"/> if instructions are valid; otherwise <see langword="false"/>.</returns>
        public static bool IsValid(string instructions)
        {
            foreach (char instruction in instructions)
            {
                if (instruction != 'R' && instruction != 'F' && instruction != 'B')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
