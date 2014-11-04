namespace WhiteBox.Kernel.App
{
    using System.Reflection;

    public abstract class BaseModule : IModule
    {
        public virtual Assembly GetAssembly()
        {
            return GetType().Assembly;
        }
    }
}
