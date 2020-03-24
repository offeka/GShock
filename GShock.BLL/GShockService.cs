using System.Collections.Generic;
using GShock.Common.Abstract;

namespace GShock.BLL
{
    public class GShockService
    {
        /// <summary>
        /// Gets the mode currently running
        /// </summary>
        public IClockMode CurrentMode => _modes.Peek();

        private readonly ICollection<IClockButton> _buttons;
        private readonly Queue<IClockMode> _modes;

        public GShockService(ICollection<IClockButton> buttons, IEnumerable<IClockMode> modes)
        {
            _buttons = buttons;
            _modes = new Queue<IClockMode>(modes);
        }
    }
}