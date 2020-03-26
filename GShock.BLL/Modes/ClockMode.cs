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
        private const int UPDATE_INTERVAL = 100;
        private DateTime _time;

        public string Title => "Clock";

        public DateTime Time
        {
            get => _time;
            private set
            {
                _time = value;
                OnChanged?.Invoke();
            }
        }

        public event Action? OnChanged;


        public ClockMode()
        {
            var updateTimer = new Timer
            {
                Interval = UPDATE_INTERVAL, AutoReset = true
            };
            updateTimer.Elapsed += (sender, args) => { Time = Time.AddMilliseconds(UPDATE_INTERVAL); };
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


        private void AddHour(TimeSpan duration) => Time = Time.AddHours(1);

        private void AddMinute(TimeSpan duration) => Time = Time.AddMinutes(1);

        private void AddSecond(TimeSpan duration) => Time = Time.AddSeconds(1);
    }
}