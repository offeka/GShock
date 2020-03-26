using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using GShock.Common.Abstract;
using GShock.Common.DTO;

namespace GShock.BLL.Modes
{
    public class StopperMode : IClockMode
    {
        private const int UPDATE_INTERVAL = 10;
        private const int LONG_HOLD = 5;
        private TimeSpan _stopper;
        private IEnumerable<TimeSpan> _results;
        private readonly Timer _timer;
        public string Title => "Stopper";
        public event Action? OnChanged;

        public StopperMode()
        {
            _timer = new Timer
            {
                AutoReset = true, Interval = UPDATE_INTERVAL
            };
            _timer.Elapsed += (sender, args) =>
                StopperState = StopperState.Add(new TimeSpan(0, 0, 0, 0, UPDATE_INTERVAL));
            _results = new List<TimeSpan>();
        }

        public IEnumerable<TimeSpan> Results
        {
            get => _results;
            set
            {
                _results = value;
                OnChanged?.Invoke();
            }
        }

        public TimeSpan StopperState
        {
            get => _stopper;
            private set
            {
                _stopper = value;
                OnChanged?.Invoke();
            }
        }

        public void OnStart(IEnumerable<IClockButton> buttons)
        {
            var buttonsCopy = buttons.ToList();
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.A)?.Subscribe(ToggleTimer);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.B)?.Subscribe(Reset);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.C)?.Subscribe(AddResult);
        }

        public void OnEnd(IEnumerable<IClockButton> buttons)
        {
            var buttonsCopy = buttons.ToList();
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.A)?.Unsubscribe(ToggleTimer);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.B)?.Unsubscribe(Reset);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.C)?.Unsubscribe(AddResult);
        }

        private void ToggleTimer(TimeSpan duration)
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

        private void Reset(TimeSpan duration)
        {
            if (duration.TotalSeconds > LONG_HOLD)
            {
                StopperState = new TimeSpan();
                _timer.Stop();
            }
        }

        private void AddResult(TimeSpan duration)
        {
            Results = Results.Append(StopperState);
        }
    }
}