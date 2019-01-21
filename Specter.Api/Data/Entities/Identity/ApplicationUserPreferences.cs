using System;

namespace Specter.Api.Data.Entities
{
    public class ApplicatioUserPreferences : ICloneable
    {
        public static ApplicatioUserPreferences Default { get; }

        public bool DarkMode { get; set; }

        static ApplicatioUserPreferences()
        {
            Default = new ApplicatioUserPreferences
            {
                DarkMode = false
            };
        }

        public object Clone()
        {
            return new ApplicatioUserPreferences
            {
                DarkMode = DarkMode
            };
        }
    }
}