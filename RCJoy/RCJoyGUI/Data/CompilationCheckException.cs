using System;

namespace Tahorg.RCJoyGUI.Data
{

    public class CompilationCheckException : Exception
    {
        public CompilationCheckException(string Message) : base(Message)
        {
            
        }

        public CompilationCheckException(string message, CompileIteration iteration) : base(message)
        {
            Iteration = iteration;
        }

        public CompilationCheckException(string message, Exception innerException, CompileIteration iteration) : base(message, innerException)
        {
            Iteration = iteration;
        }

        public enum CompileIteration
        {
            ArrayMapping,
            EEPMaping,
            Chaining,
            Generating_Defines,
            Generating_Classes,
            Generating_Code,
            PreCheck
        }

        public CompileIteration Iteration { get; set; }
    }
}
