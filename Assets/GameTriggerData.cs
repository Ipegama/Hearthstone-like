using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu]
    public class GameTriggerData : ScriptableObject
    {
        [Header("Events")]
        public GameEventData gameEvent;

        public ConditionData[] condition;
        public ActionData[] actions;

        public string GetDescription()
        {
            var cardName = "a creature";

            return $"{gameEvent.GetDescription(cardName) + actions[0].GetDescription()}";
        }
    }
}
