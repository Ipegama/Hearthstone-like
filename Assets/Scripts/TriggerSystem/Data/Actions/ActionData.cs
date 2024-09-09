using UnityEngine;

namespace TriggerSystem.Data
{
    public abstract class ActionData : ScriptableObject
    {
        public abstract void Execute(ActionContext context);

        public abstract string GetDescription();
    }
}