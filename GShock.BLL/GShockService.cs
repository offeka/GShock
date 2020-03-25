using System.Collections.Generic;
using System.Linq;
using GShock.Common.Abstract;
using GShock.Common.DTO;

namespace GShock.BLL
{
    public class GShockService
    {
        /// <summary>
        /// Gets the mode currently running
        /// </summary>
        public IClockMode CurrentMode => _modes.Peek();

        public IEnumerable<IClockButton> Buttons { get; }
        private readonly Queue<IClockMode> _modes;

        public GShockService(IEnumerable<IClockButton> buttons, IEnumerable<IClockMode> modes)
        {
            Buttons = buttons.ToList();
            Buttons.FirstOrDefault(button => button.Type == ButtonType.S)?.Subscribe(NextMode);
            _modes = new Queue<IClockMode>(modes);
        }

        private void NextMode(int duration)
        {
            var currentMode = _modes.Dequeue();
            currentMode.OnEnd();
            _modes.Peek().OnStart(Buttons.ToList());
        }
    }
}