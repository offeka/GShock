using System;
using GShock.Common.DTO;

namespace GShock.Common.Abstarct
{
    public interface IClockButton
    {
        ButtonType Type { get; }

        /// <summary>
        /// Subscribe a delegate to a button action
        /// </summary>
        /// <param name="buttonAction">The action that decides if <param name="clickHandler"></param> is called</param>
        /// <param name="clickHandler">the Action to run on a button click based on the delegate</param>
        void Subscribe(Action<Action, int> buttonAction, EventHandler clickHandler);

        /// <summary>
        /// Removes an action from the internal action store
        /// </summary>
        /// <param name="buttonAction">The action to remove</param>
        void Unsubscribe(Action<Action, int> buttonAction);
    }
}