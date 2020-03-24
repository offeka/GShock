using System;
using GShock.Common.DTO;

namespace GShock.Common.Abstract
{
    public interface IClockButton
    {
        ButtonType Type { get; }

        /// <summary>
        /// Subscribe a delegate to a button action
        /// </summary>
        /// <param name="buttonAction">The action runs when a button is pressed</param>
        void Subscribe(Action<int> buttonAction);

        /// <summary>
        /// Removes an action from the internal action store
        /// </summary>
        /// <param name="buttonAction">The action to remove</param>
        void Unsubscribe(Action<int> buttonAction);
        
    }
}