using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu]
    public class BuffData : ScriptableObject
    {
        public int attack;
        public int maxHealth;

        public Buff Create()
        {
            return new Buff(this);
        }

        public string GetDescription()
        {
            var list = new List<string>();
            if (attack != 0) list.Add($"{attack} attack");
            if (maxHealth != 0) list.Add($"{maxHealth} maximum health");
            return string.Join(" and ", list);
        }
    }
}
