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
        private Stack<Action<long>> _actionStore;

        public Button(ButtonType type)
        {
            _actionStore = new Stack<Action<long>>();
            Type = type;
        }

        public void Subscribe(Action<long> buttonAction) => _actionStore.Push(buttonAction);

        public void Unsubscribe(Action<long> buttonAction)
        {
            List<Action<long>> actionList = _actionStore.ToList();
            actionList.ToList().Remove(buttonAction);
            _actionStore = new Stack<Action<long>>(actionList);
        }

        public void OnClick(long duration)
        {
            foreach (Action<long> action in _actionStore)
            {
                action(duration);
            }
        }
    }
}