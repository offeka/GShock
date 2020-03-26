using System;
using GShock.BLL.Buttons;
using GShock.Common.Abstract;
using GShock.Common.DTO;
using Microsoft.Extensions.DependencyInjection;

namespace GShock.BLL.ExtensionMethods
{
    public static class ButtonExtensionMethods
    {
        /// <summary>
        /// Add the default gshock buttons to the service collection in a nice wrapper
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddButtons(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IClockButton>(new Button(ButtonType.S));
            serviceCollection.AddSingleton<IClockButton>(new Button(ButtonType.A));
            serviceCollection.AddSingleton<IClockButton>(new Button(ButtonType.B));
            serviceCollection.AddSingleton<IClockButton>(new Button(ButtonType.C));
            return serviceCollection;
        }
    }
}