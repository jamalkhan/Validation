using System;
namespace bleak.Validation
{
    public class Error
    {
        public Error()
        {
            DebugDetails = new DebugDetails();
        }

        public DebugDetails DebugDetails { get; set; }

        public string FriendlyMessage
        {
            get
            {
                return DebugDetails.Exception.Message;
            }
            set
            {
            }
        }

        /// <summary>
        /// Used for localization
        /// </summary>
        public string FriendlyMessageResourceType { get; set; }

        /// <summary>
        /// Used for localization
        /// </summary>
        public string FriendlyMessageResourceName { get; set; }
    }

}
