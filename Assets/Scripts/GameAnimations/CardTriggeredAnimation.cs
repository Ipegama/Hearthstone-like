using Gameplay;
using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class CardTriggeredAnimation : GameAnimation
    {
        private Card _card;
        public CardTriggeredAnimation(Card card)
        {
            _card = card;
        }

        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(.4f);
            _card.UI.Trigger(0.4f, 2f);
            yield return new WaitForSeconds(.4f);
        }

        public override void ExecuteWithoutAnimation()
        {
            
        }
    }
}
