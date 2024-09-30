using Gameplay;
using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class ManaCostChangedAnimation : GameAnimation
    {
        private Card _card;
        private int _amount;

        public ManaCostChangedAnimation(Card card, int amount)
        {
            _card = card;
            _amount = amount;
        }

        public override IEnumerator Execute()
        {
            _card.UI.SetManaCost(_amount);
            yield return new WaitForSeconds(0.2f);
        }

        public override void ExecuteWithoutAnimation()
        {
            _card.UI.SetManaCost(_amount);
        }
    }
}
