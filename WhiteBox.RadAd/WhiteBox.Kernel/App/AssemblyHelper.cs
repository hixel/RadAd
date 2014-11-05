namespace WhiteBox.Kernel.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AssemblyHelper
    {
        public static IEnumerable<IModule> GetModules()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(x => x.IsClass
                                    && !x.IsAbstract
                                    && typeof (IModule).IsAssignableFrom(x))
                        .Select(x => Activator.CreateInstance(x) as IModule);
        }
    }
}
