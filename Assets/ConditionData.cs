using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem.Data.Conditions
{
    public abstract class ConditionData : ScriptableObject
    {
        public ConditionOperator op;
        public abstract bool Check(ActionContext context);
    }

    public enum ConditionOperator
    {
        Equals, NotEquals
    }
}
