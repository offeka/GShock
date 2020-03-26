using System;
using System.Collections.Generic;
using System.Linq;
using GShock.Common.Abstract;
using GShock.Common.DTO;

namespace GShock.BLL.Buttons
{
    public class Button : IClockButton
    {
        public ButtonType Type { get; }
        private Stack<Action<TimeSpan>> _actionStore;

        public Button(ButtonType type)
        {
            _actionStore = new Stack<Action<TimeSpan>>();
            Type = type;
        }

        public void Subscribe(Action<TimeSpan> buttonAction) => _actionStore.Push(buttonAction);

        public void Unsubscribe(Action<TimeSpan> buttonAction)
        {
            List<Action<TimeSpan>> actionList = _actionStore.ToList();
            actionList.Remove(buttonAction);
            _actionStore = new Stack<Action<TimeSpan>>(actionList);
        }

        public void OnClick(TimeSpan duration)
        {
            foreach (Action<TimeSpan> action in _actionStore)
            {
                action(duration);
            }
        }
    }
}