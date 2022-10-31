using System.Resources;

namespace MDeque
{
    partial class Program
    {
        private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(Program));

        private static string? SequencePrompt
        {
            get
            {
                return s_resourceManager.GetString(nameof(SequencePrompt));
            }
        }

        private static string? InstructionsPrompt
        {
            get
            {
                return s_resourceManager.GetString(nameof(InstructionsPrompt));
            }
        }

        private static string? InvalidInstructionsMessage
        {
            get
            {
                return s_resourceManager.GetString(nameof(InvalidInstructionsMessage));
            }
        }

        private static string? OutputMessage
        {
            get
            {
                return s_resourceManager.GetString(nameof(OutputMessage));
            }
        }

        private static string? EmptySequenceMessage
        {
            get
            {
                return s_resourceManager.GetString(nameof(EmptySequenceMessage));
            }
        }
    }
}
