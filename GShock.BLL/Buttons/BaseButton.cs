using System;
using System.Collections.Generic;
using GShock.Common.Abstract;
using GShock.Common.DTO;

namespace GShock.BLL.Buttons
{
    public class Button : IClockButton
    {
        public ButtonType Type { get; }
        protected ICollection<Action<int>> ActionStore;

        protected Button(ButtonType type)
        {
            ActionStore = new List<Action<int>>();
            Type = type;
        }

        public void Subscribe(Action<int> buttonAction)
        {
            ActionStore.Add(buttonAction);
        }

        public void Unsubscribe(Action<int> buttonAction)
        {
            ActionStore.Remove(buttonAction);
        }

        public virtual void OnClick(int duration)
        {
            foreach (var action in ActionStore)
            {
                action(duration);
            }
        }
    }
}