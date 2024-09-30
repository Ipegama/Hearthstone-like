using DG.Tweening;
using Gameplay.Interfaces;
using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class MaxHealthChangedAnimation : GameAnimation
    {
        private ITargetable _card;
        private int _amount;

        private int _health;
        private int _maxHealth;

        public MaxHealthChangedAnimation(ITargetable card, int amount, int health, int maxHealth)
        {
            _card = card;
            _amount = amount;
            _health = health;
            _maxHealth = maxHealth;
        }

        public override IEnumerator Execute()
        {
            _card.SetHealth(_health, _maxHealth);
            yield return new WaitForSeconds(0.2f);
        }

        public override void ExecuteWithoutAnimation()
        {
            _card.SetHealth(_health, _maxHealth);
        }
    }
}