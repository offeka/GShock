using System;
using System.Collections.Generic;

namespace GShock.Common.Abstract
{
    public interface IClockMode
    {
        string Title { get; }

        /// <summary>
        /// Called when you enter the mode from the controller
        /// </summary>
        /// <param name="buttons">The clock default buttons</param>
        void OnStart(IEnumerable<IClockButton> buttons);

        /// <summary>
        /// Called when the mode exits
        /// </summary>
        public void OnEnd(IEnumerable<IClockButton> buttons);
    }
}