using System;
using System.Collections.Generic;
using System.Timers;
using GShock.Common.Abstract;
using GShock.Common.DTO;
using static System.DateTime;

namespace GShock.BLL.Modes
{
    public class ClockMode : IClockMode
    {
        private const int UPDATE_INTERVAL = 10;

        public string Title => "Clock";

        public DateTime Time { get; private set; }


        public ClockMode()
        {
            var updateTimer = new Timer
            {
                Interval = UPDATE_INTERVAL, AutoReset = true, Enabled = true
            };
            updateTimer.Elapsed += (sender, args) => { Time = Time.AddMilliseconds(UPDATE_INTERVAL); };
        }

        public void OnStart(ICollection<IClockButton> buttons)
        {
            foreach (var clockButton in buttons)
            {
                var addTime = GetAddedInterval(clockButton);
                clockButton.Subscribe(duration => AddTime(GetAddedInterval(clockButton)));
            }
        }

        private void AddTime(TimeSpan timeToAdd)
        {
            Time += timeToAdd;
        }

        private static TimeSpan GetAddedInterval(IClockButton button)
        {
            return button.Type switch
            {
                ButtonType.A => new TimeSpan(0, 1, 0, 0),
                ButtonType.B => new TimeSpan(0, 0, 1, 0),
                ButtonType.C => new TimeSpan(0, 0, 0, 1),
                _ => new TimeSpan(0)
            };
        }
    }
}