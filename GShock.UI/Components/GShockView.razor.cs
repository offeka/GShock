using System;
using System.Collections.Generic;
using System.Linq;
using GShock.Common.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

namespace GShock.UI.Components
{
    public partial class GShockView
    {
        [Inject] public NavigationManager Manager { get; set; }

        private Dictionary<ButtonType, DateTime> _clickTimes;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _clickTimes = new Dictionary<ButtonType, DateTime>();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            Manager.NavigateTo(GShock.CurrentMode.Title);
        }

        private void ClickDurationDown(ButtonType type)
        {
            _clickTimes[type] = DateTime.Now;
        }

        private void ClickDurationUp(ButtonType type)
        {
            var clickDuration = DateTime.Now - _clickTimes[type];
            GShock.Buttons.FirstOrDefault(button => button.Type == type)?.OnClick(clickDuration);
            Manager.NavigateTo(GShock.CurrentMode.Title);
        }
    }
}