using System;
using System.Collections.Generic;

namespace bleak.Validation
{
    [Serializable]
    public abstract class BaseFailedValidation : IFailedValidation
    {
        /// <summary>
        /// These are the field names associated with this validation issue.
        /// </summary>
        public IList<string> FieldNames { get; set; } = new List<string>();

        /// <summary>
        /// This is the error message displayed to the user.
        /// </summary>
        public virtual string ErrorMessage { get; set; }

        /// <summary>
        /// If there is some additional information we can provide to the user, let's provide it here.
        /// </summary>
        public virtual string ExtraHelp { get; set; }
    }

}
