using Gameplay;
using UnityEngine;
using System.Collections;

namespace GameAnimations
{
    public class DamageAnimation : GameAnimation 
    {
        private Card _card;
        private int _damageAmount;

        private int _health;
        private int _maxHealth;

        public DamageAnimation(Card card, int damageAmount, int health, int maxHealth)
        {
            _card = card;
            _damageAmount = damageAmount;
            _health = health;
            _maxHealth = maxHealth;
        }

        public override IEnumerator Execute()
        {
            var prefab = AnimationsQueue.Instance.damageCanvasPrefab;
            var targetPosition = _card.transform.position;
            targetPosition.y += 0.1f;

            var damageCanvas = Object.Instantiate(prefab, targetPosition, prefab.transform.rotation);

            _card.UI.SetHealth(_health,_maxHealth);
            yield return damageCanvas.AnimateCoroutine(_damageAmount);
        }

        public override void ExecuteWithoutAnimation()
        {
           _card.UI.SetHealth(_health,_maxHealth);
        }

    }

}
