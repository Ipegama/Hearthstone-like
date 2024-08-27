using System;
using Gameplay;

namespace TriggerSystem
{
    [Serializable]
    public class ActionContext
    {
        public Card source;
        public Card target;

        public Card triggeringCard;

        public Action<ActionContext> sourceAction;

        public ActionContext(ActionContext copy)
        {
            source = copy.source;
            target = copy.target;

            sourceAction = copy.sourceAction;
        }

        public T Get<T>(TargetCreatureType type) where T : class
        {
            switch (type)
            {
                case TargetCreatureType.TriggerCreature:
                    return triggeringCard as T;
                default:
                    throw new ArgumentException();
            }
        }
    }

}
