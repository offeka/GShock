using System;
using System.Collections.Generic;
using System.Linq;
using GShock.Common.DTO;
using Microsoft.AspNetCore.Components.Web;

namespace GShock.UI.Components
{
    public partial class GShockView
    {
        private Dictionary<ButtonType, DateTime> _clickTimes;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _clickTimes = new Dictionary<ButtonType, DateTime>();
        }

        private void ClickDurationDown(ButtonType type)
        {
            _clickTimes[type] = DateTime.Now;
        }

        private void ClickDurationUp(ButtonType type)
        {
            var clickDuration = (new DateTime() - _clickTimes[type]).Ticks;
            GShock.Buttons.FirstOrDefault(button => button.Type == type)?.OnClick(clickDuration);
        }
    }
}