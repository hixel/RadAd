namespace WhiteBox.Kernel.Attributes
{
    using System;

    public class DisplayAttribute : Attribute
    {
        private readonly string displayName;

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public DisplayAttribute(string displayName)
        {
            this.displayName = displayName;
        }
    }
}
