using GShock.BLL.Modes;
using GShock.Common.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace GShock.BLL.ExtensionMethods
{
    public static class ModeExtensionMethod
    {
        public static IServiceCollection AddModes(this IServiceCollection serviceCollection)
        {
            var clockMode = new ClockMode();
            serviceCollection.AddSingleton<IClockMode>(clockMode);
            serviceCollection.AddSingleton(clockMode);
            return serviceCollection;
        }
    }
}