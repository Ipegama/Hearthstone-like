using System;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu]
    public class GameEventData : ScriptableObject
    {
        public string description;

        private List<GameTrigger> _triggers = new List<GameTrigger>();

        public void Raise(ActionContext context)
        {
            foreach (var trigger in _triggers)
            {
                trigger.Trigger(context);
            }
        }
        public void AddTrigger(GameTrigger trigger)
        {
            _triggers.Add(trigger);
        }

        public void RemoveTrigger(GameTrigger trigger)
        {
            _triggers.Remove(trigger);  
        }

        public int GetTriggersCount()
        {
            return _triggers.Count;
        }

        public void ClearTriggers()
        {
            _triggers.Clear();
        }

        public string GetDescription(string cardName)
        {
            return string.Format(description,cardName);
        }
    }
}
