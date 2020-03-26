using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GShock.Common.Abstract;
using GShock.Common.DTO;

namespace GShock.BLL.Modes
{
    public class DateMode : IClockMode
    {
        private const int LONG_HOLD_TIME = 3;
        public string Title => "Date";
        public event Action? OnChanged;
        private DateTime _date;

        public DateMode()
        {
            Date = DateTime.Now;
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnChanged?.Invoke();
            }
        }


        public void OnStart(IEnumerable<IClockButton> buttons)
        {
            var buttonsCopy = buttons.ToList();
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.A)?.Subscribe(AddMonth);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.B)?.Subscribe(AddDay);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.C)?.Subscribe(OnCClick);
        }

        public void OnEnd(IEnumerable<IClockButton> buttons)
        {
            var buttonsCopy = buttons.ToList();
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.A)?.Unsubscribe(AddMonth);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.B)?.Unsubscribe(AddDay);
            buttonsCopy.FirstOrDefault(button => button.Type == ButtonType.C)?.Unsubscribe(OnCClick);
        }

        private void OnCClick(TimeSpan duration)
        {
            Date = duration.TotalSeconds >= LONG_HOLD_TIME ? Date.AddYears(-1) : Date.AddYears(1);
        }

        private void AddDay(TimeSpan duration)
        {
            Date = Date.AddDays(1);
        }

        private void AddMonth(TimeSpan duration)
        {
            Date = Date.AddMonths(1);
        }
    }
}