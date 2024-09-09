using Gameplay;
using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class AttackChangedAnimation : GameAnimation
    {
        private Card _card;
        private int _amount;
        private int _attack;

        public AttackChangedAnimation(Card card, int amount, int attack)
        {
            _card = card;
            _amount = amount;
            _attack = attack;
        }

        public override IEnumerator Execute()
        {
           _card.UI.SetAttack(_attack);
            yield return new WaitForSeconds(0.2f);
        }

        public override void ExecuteWithoutAnimation()
        {
            _card.UI.SetAttack(_attack);
        }
    }
}

