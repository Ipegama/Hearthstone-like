using Gameplay;
using System;
using TriggerSystem.Data;

namespace TriggerSystem
{
    [Serializable]
    public class GameTrigger
    {
        public GameEventData gameEvent;
        public ActionData[] actions;

        private GameTriggerData _data;
        private Card _card;

        public GameTrigger(GameTriggerData trigger, Card card)
        {
            _data = trigger;

            gameEvent = trigger.gameEvent;
            actions = new ActionData[trigger.actions.Length];
            for (int i = 0; i < trigger.actions.Length; i++)
            {
                actions[i] = trigger.actions[i];
            }

            _card = card;
        }

        public void Trigger(ActionContext context)
        {
            context.thisCard = _card;
            if (!CheckConditions(context)) return;

            Events.Cards.Triggered?.Invoke(_card);
            foreach(var action in actions)
            {
                action.Execute(context);
            }
        }

        public bool CheckConditions(ActionContext context)
        {
            foreach(var condition in _data.conditions)
            {
                if(!condition.Check(context)) return false;
            }

            return true;
        }
    }
}
