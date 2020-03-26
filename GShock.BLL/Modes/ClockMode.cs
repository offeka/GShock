using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using GShock.Common.Abstract;
using GShock.Common.DTO;
using static System.DateTime;

namespace GShock.BLL.Modes
{
    public class ClockMode : IClockMode
    {
        private const long UPDATE_INTERVAL = 100;

        public string Title => "Clock";

        public DateTime Time { get; private set; }

        public event Action? OnChanged;


        public ClockMode()
        {
            var updateTimer = new Timer
            {
                Interval = UPDATE_INTERVAL, AutoReset = true
            };
            updateTimer.Elapsed += (sender, args) =>
            {
                Time = Time.AddMilliseconds(UPDATE_INTERVAL);
                OnChanged?.Invoke();
            };
            updateTimer.Start();
            Time = Now;
        }

        public void OnStart(IEnumerable<IClockButton> buttons)
        {
            List<IClockButton> buttonsCopy = buttons.ToList();
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.A)?.Subscribe(AddHour);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.B)?.Subscribe(AddMinute);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.C)?.Subscribe(AddSecond);
        }

        public void OnEnd(IEnumerable<IClockButton> buttons)
        {
            List<IClockButton> buttonsCopy = buttons.ToList();
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.A)?.Unsubscribe(AddHour);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.B)?.Unsubscribe(AddMinute);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.C)?.Unsubscribe(AddSecond);
        }


        private void AddHour(long duration) => Time = Time.AddHours(1);

        private void AddMinute(long duration) => Time = Time.AddMinutes(1);

        private void AddSecond(long duration) => Time = Time.AddSeconds(1);
    }
}