using System;
using System.Collections.Generic;
using System.Text;

namespace USFMToolsSharp.Models
{
    public enum LinterLevel
    {
        Info,
        Warning,
        Error,
    }
    public class LinterResult
    {
        public LinterLevel Level;
        public string Message;
        public int Position;

        public LinterResult()
        {

        }

        public LinterResult(LinterLevel level, string message, int position)
        {
            Level = level;
            Message = message;
            Position = position;
        }
    }
}
