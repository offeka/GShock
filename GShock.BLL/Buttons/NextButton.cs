using System;
using GShock.Common.DTO;

namespace GShock.BLL.Buttons
{
    public class NextButton : Button
    {
        private Action _next;

        protected NextButton(ButtonType type, Action next) : base(type)
        {
            _next = next;
        }

        public override void OnClick(int duration)
        {
            base.OnClick(duration);
            _next();
        }
    }
}