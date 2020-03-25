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
        private Stack<Action<int>> _actionStore;

        public Button(ButtonType type)
        {
            _actionStore = new Stack<Action<int>>();
            Type = type;
        }

        public void Subscribe(Action<int> buttonAction)
        {
            _actionStore.Push(buttonAction);
        }

        public void Unsubscribe(Action<int> buttonAction)
        {
            List<Action<int>> actionList = _actionStore.ToList();
            actionList.ToList().Remove(buttonAction);
            _actionStore = new Stack<Action<int>>(actionList);
        }

        public virtual void OnClick(int duration)
        {
            foreach (Action<int> action in _actionStore)
            {
                action(duration);
            }
        }
    }
}