using Gameplay;
using TriggerSystem.Data;
using UnityEngine;

namespace TriggerSystem
{
    [CreateAssetMenu(fileName = "Add Trigger Buff", menuName = "Buffs/Add Trigger Buff")]
    public class AddTriggerBuff : Buff
    {
        private AddTriggerBuffData _data;
        public AddTriggerBuff(AddTriggerBuffData addTriggerBuffData) 
        { 
            _data = addTriggerBuffData;
        }

        public override void OnApply(IBuffable entity)
        {
            if (entity is Creature creature)
            {
                creature.AddTrigger(_data.trigger);
            }
        }

        public override void OnRemove(IBuffable entity)
        {
            if (entity is Creature creature)
            {
                creature.RemoveTrigger(_data.trigger);
            }
        }
    }
}