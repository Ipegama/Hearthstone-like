using TriggerSystem;
using TriggerSystem.Data;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "New Hero Power", menuName = "Gameplay/Hero Power")]
    public class HeroPowerData : ScriptableObject
    {
        public string powerName;
        public Sprite powerIcon;
        public int manaCost;
        public TargetFilter targetFilter;
        public ActionData[] playActions;
    }
}
