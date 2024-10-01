using DG.Tweening;
using Gameplay.Interfaces;
using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class HealAnimation : GameAnimation
    {
        private ITargetable _card;
        private int _amount;

        private int _health;
        private int _maxHealth;

        public HealAnimation(ITargetable card, int amount, int health, int maxHealth)
        {
            _card = card;
            _amount = amount;
            _health = health;
            _maxHealth = maxHealth;
        }

        public override IEnumerator Execute()
        {
            if(_amount == 0) yield break;

            var prefab = AnimationsQueue.Instance.healCanvasPrefab;
            var transform = _card.GetTransform();

            var healCavnas = Object.Instantiate(prefab);
            healCavnas.SetFakeParent(transform);
            _card.SetHealth(_health,_maxHealth);
            healCavnas.StartAnimateCoroutine(_amount,"+");
            transform.DOPunchScale(Vector3.one * 0.01f * 0.5f, 0.4f);
        }

        public override void ExecuteWithoutAnimation()
        {
            _card.SetHealth(_health, _maxHealth);
        }
    }
}