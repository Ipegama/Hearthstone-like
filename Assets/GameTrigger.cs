using System;
using TriggerSystem.Data;

namespace TriggerSystem
{
    [Serializable]
    public class GameTrigger
    {
        public GameEventData gameEvent;
        public ActionData[] actions;

        public void Trigger(ActionContext context)
        {
            foreach (var action in actions)
            {
                action.Execute(context);
            }
        }
    }
}
