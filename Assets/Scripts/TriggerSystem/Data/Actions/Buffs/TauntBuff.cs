﻿using Gameplay;
using TriggerSystem.Data;
namespace TriggerSystem
{
    public class TauntBuff : Buff
    {
        public TauntBuff(TauntBuffData buffData)
        {

        }
        public override void OnApply(IBuffable entity)
        {
            if (entity is Creature creature)
            {
                creature.SetTaunt();
            }
        }

        public override void OnRemove(IBuffable entity)
        {
            if (entity is Creature creature)
            {
                creature.SetTaunt();
            }
        }
    }
}
