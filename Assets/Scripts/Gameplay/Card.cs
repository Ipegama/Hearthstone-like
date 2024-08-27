using System;
using Data;
using TriggerSystem;
using UnityEngine;

namespace Gameplay
{
    public class Card : MonoBehaviour
    {
        private CardData _cardData;
        public CardData CardData => _cardData;

        public CardUI UI;
        public Player owner;
        public Zone container;

        private int _health;
        private int _maxHealth;
        private int _attack;

        public event Action<bool> HighlightChanged;
        public event Action<bool> SelectChanged;

        private void Awake()
        {
            UI = GetComponent<CardUI>();
        }
        public void SetData(CardData data)
        {
            _cardData = data;

            _maxHealth = _cardData.maxHealth;
            _health = _maxHealth;
            _attack = _cardData.attack;
        }

        private void ExecuteOnPlayActions(Card target)
        {
          /*  if (!_cardData.HasTarget) return;

            if (target == null)
            {
                throw new Exception("Target needed");
            }

            foreach (var action in _cardData.playActions)
            {
                source = this;
                target = target;
            }*/
        }

        public void SetOwner(Player owner)
        {
            this.owner = owner;
            transform.SetParent(this.owner.transform, true);
        }

        public void SetZone(Zone zone)
        {
            if (container != null)
            {
                container.RemoveCard(this);
            }
            container = zone;

           // UI.SetCardBack(zone.containerData.showBack);
        }

        public void Play(Card target)
        {
            Events.Cards.Played?.Invoke(this);
            Unhighlight();
            ExecuteOnPlayActions(target);
        }

        public void Damage(int amount)
        {
            _health -= amount;
            Events.Creatures.Damaged?.Invoke(this, amount, _health, _maxHealth);

            if (_health <= 0)
            {
                Events.Creatures.Death?.Invoke(this);
            }
        }

        public void Unhighlight()
        {
            HighlightChanged?.Invoke(false);
        }

        public void Highlight()
        {
            HighlightChanged?.Invoke(true);
        }

        public void Unselect()
        {
            SelectChanged?.Invoke(false);
        }

        public void Select()
        {
            SelectChanged?.Invoke(true);
        }
    }
}
