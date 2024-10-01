using UnityEngine;

namespace TriggerSystem.Data
{
    public abstract class BuffData : ScriptableObject
    {
        public abstract Buff Create();

        public abstract string GetDescription();
    }
}
