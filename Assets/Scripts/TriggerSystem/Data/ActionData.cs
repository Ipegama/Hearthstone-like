using System;
using UnityEngine;

namespace TriggerSystem
{
    public class ActionData : ScriptableObject
    {
        public virtual void Execute(ActionContext context)
        {

        }

        public virtual string GetDescription()
        {
            return $"-Empty-";
        }
    }
}