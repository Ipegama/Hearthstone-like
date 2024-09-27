using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "Repeat", menuName = "Actions/Repeat")]
    public class RepeatActionData : ActionData
    {
        public int repeatTimes;
        public ActionData actionToRepeat;

        public override void Execute(ActionContext context)
        {
            for (int i = 0; i < repeatTimes; i++)
            {
                actionToRepeat.Execute(context);
            }
        }

        public override string GetDescription()
        {
            string description = "";
            description += actionToRepeat.GetDescription();
            description += $" {repeatTimes} times.";
            return description;
        }
    }
}
