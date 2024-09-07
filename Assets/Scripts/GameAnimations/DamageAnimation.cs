using Gameplay.Interfaces;
using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class DamageAnimation : GameAnimation 
    {
        private ITargetable _card;
        private int _damageAmount;

        private int _health;
        private int _maxHealth;

        public DamageAnimation(ITargetable card, int damageAmount, int health, int maxHealth)
        {
            _card = card;
            _damageAmount = damageAmount;
            _health = health;
            _maxHealth = maxHealth;
        }

        public override IEnumerator Execute()
        {
            if(_damageAmount == 0) yield break;

            var prefab = AnimationsQueue.Instance.damageCanvasPrefab;
            var transform = _card.GetTransform();

            var damageCanvas = Object.Instantiate(prefab);
            damageCanvas.SetFakeParent(transform);
            _card.SetHealth(_health,_maxHealth);
            damageCanvas.StartAnimateCoroutine(_damageAmount, "-");
            _card.AnimateDamage(Vector3.one * 0.01f * 0.5f, 0.4f);
        }

        public override void ExecuteWithoutAnimation()
        {
            _card.SetHealth(_health, _maxHealth);
        }
    }
}
