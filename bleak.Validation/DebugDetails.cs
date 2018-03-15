using System;
namespace bleak.Validation
{
    public class DebugDetails
    {
        public string CodeFile { get; set; }

        public int LineNumber { get; set; }

        public string ObjectName { get; set; }

        public string PropertyName { get; set; }

        public Exception Exception { get; set; }
    }
}
