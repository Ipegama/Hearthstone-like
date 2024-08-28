using System;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu]
    public class GameEventData : ScriptableObject
    {
        private readonly List<Action<ActionContext>> _listeners = new List<Action<ActionContext>>();

        public void Raise(ActionContext context)
        {
            foreach (var listener in _listeners)
            {
                listener?.Invoke(context);
            }
        }

        public void AddListener(Action<ActionContext> listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(Action<ActionContext> listener)
        {
            _listeners.Remove(listener);
        }
    }
}
