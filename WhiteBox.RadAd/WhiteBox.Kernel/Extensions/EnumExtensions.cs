namespace WhiteBox.Kernel.Extensions
{
    using System;
    using Attributes;

    public static class EnumExtensions
    {
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            return (T)attributes[0];
        }

        public static string GetDisplayName(this Enum value)
        {
            var attribute = value.GetAttribute<DisplayAttribute>();
            return attribute == null ? value.ToString() : attribute.DisplayName;
        }
    }
}
