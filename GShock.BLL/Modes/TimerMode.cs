using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using GShock.Common.Abstract;
using GShock.Common.DTO;

namespace GShock.BLL.Modes
{
    public class TimerMode : IClockMode
    {
        private readonly Timer _timer;
        private const int UPDATE_INTERVAL = 10;
        private const int LONG_HOLD_TIME = 3;
        private TimeSpan _timerState;
        public string Title => "Timer";
        public event Action? OnChanged;

        public TimeSpan TimerState
        {
            get => _timerState;
            private set
            {
                _timerState = value;
                OnChanged?.Invoke();
            }
        }


        public TimerMode()
        {
            _timer = new Timer
            {
                Interval = UPDATE_INTERVAL, AutoReset = true
            };
            _timer.Elapsed += (sender, args) =>
            {
                TimerState = TimerState.Subtract(new TimeSpan(0, 0, 0, 0, UPDATE_INTERVAL));
                if (TimerState.TotalMilliseconds < 0)
                {
                    TimerState = new TimeSpan();
                    _timer.Stop();
                }
            };
        }

        public void OnStart(IEnumerable<IClockButton> buttons)
        {
            var buttonsCopy = buttons.ToList();
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.A)?.Subscribe(AddMinute);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.B)?.Subscribe(AddHour);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.C)?.Subscribe(OnAClick);
        }

        public void OnEnd(IEnumerable<IClockButton> buttons)
        {
            var buttonsCopy = buttons.ToList();
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.A)?.Unsubscribe(AddMinute);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.B)?.Unsubscribe(AddHour);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.C)?.Unsubscribe(OnAClick);
        }

        private void AddMinute(TimeSpan duration)
        {
            TimerState = TimerState.Add(new TimeSpan(0, 0, 1, 0));
        }

        private void AddHour(TimeSpan duration)
        {
            TimerState = TimerState.Add(new TimeSpan(0, 1, 0, 0));
        }

        private void OnAClick(TimeSpan duration)
        {
            if (duration.TotalSeconds >= LONG_HOLD_TIME)
            {
                TimerState = new TimeSpan();
            }
            else
            {
                ToggleTimer();
            }
        }

        private void ToggleTimer()
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
            }
            else
            {
                _timer.Start();
            }
        }
    }
}