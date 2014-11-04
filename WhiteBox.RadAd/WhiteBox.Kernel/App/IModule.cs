namespace WhiteBox.Kernel.App
{
    using System.Reflection;

    public interface IModule
    {
        Assembly GetAssembly();
    }
}
