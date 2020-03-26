using System.Timers;
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
            var timerMode = new TimerMode();
            serviceCollection.AddSingleton<IClockMode>(timerMode);
            serviceCollection.AddSingleton(timerMode);
            return serviceCollection;
        }
    }
}