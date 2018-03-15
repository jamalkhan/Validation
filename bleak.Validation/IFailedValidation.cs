using System.Collections.Generic;

namespace bleak.Validation
{
    public interface IFailedValidation
    {
        IList<string> FieldNames { get; set; }
        string ErrorMessage { get; set; }
        string ExtraHelp { get; set; }
    }
}
