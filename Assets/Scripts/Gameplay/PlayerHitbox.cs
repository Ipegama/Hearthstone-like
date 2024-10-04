using Gameplay;
using Gameplay.Interfaces;
using TriggerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class PlayerHitbox : MonoBehaviour, IHighlightable, ITargetable
    {
        public Player player;
        public Image playerBackground;
        private Color _defaultBackgroundColor;

        private void Awake()
        {
            _defaultBackgroundColor = playerBackground.color;
        }
        public bool CanBeHighlighted(Player controllerPlayer, Card selectedCard) => true;
        public void Highlight(bool value)
        {
            if (value)
            {
                playerBackground.color = Color.blue;
            }
            else
            {
                playerBackground.color = _defaultBackgroundColor;
            }
        }
        public Player GetOwner() => player;
        public Card GetCard() => null;
        public Transform GetTransform() => transform;
        public void AddBuff(Buff buff) { }
        public void Damage(int amount, bool triggerEvent, ITargetable source) => player.Damage(amount, triggerEvent,source);
        public void Heal(int amount, bool triggerEvent) => player.Heal(amount,triggerEvent);
        public void SetHealth(int health, int maxHealth) { }
        public int GetAttack() => 0;
        public Player GetPlayer() => player;
        public void AnimateDamage(Vector3 scale, float duration) { }
        public void Kill() { }
        public bool CanBeTargeted() => true;
        public bool IsCreature() => false;
        public bool IsSpell() => false;
        public bool IsPlayer() => true;
    }
}
