using System;
using System.Collections.Generic;
using Gameplay.Data;
using Gameplay.Interfaces;
using TriggerSystem;
using UnityEngine;

namespace Gameplay
{
    public class Card : MonoBehaviour, IHighlightable, ITargetable, IBuffable
    {
        public CardData CardData { get; protected set; }
        public CardType CardType { get; protected set; }
        
        [HideInInspector] public CardUI UI;
        [HideInInspector] public Player owner;
        [HideInInspector] public Zone zone;

        protected List<Buff> _buffs = new List<Buff>();
        protected bool _isDead;

        private void Awake()
        {
            UI = GetComponent<CardUI>();
            UI.SetCard(this);
        }

        private void OnDestroy()
        {
            _isDead = true;
            OnCardDestroyed();
        }
        public virtual void SetData(CardData data)=> CardData = data;
 
        private void ExecuteOnPlayAction(ITargetable target)
        {
            if (target == null)
            {
                if (CardData.targetFilter.HasTarget())
                {
                    throw new Exception("Target needed");
                }
            }

            foreach (var action in CardData.playActions)
            {
                action.Execute(new ActionContext
                {
                    TargetEntity = target,
                    thisCard = this,
                    TriggerEntity = this
                });
            }
        }

        public void SetOwner(Player player)
        {
            owner = player;
            transform.SetParent(owner.transform, true);
        }

        public void SetZone(Zone newZone)
        {
            if (zone != null)
            {
                zone.RemoveCard(this);
            }
            zone = newZone;
        }

        public void Play(ITargetable target)
        {
            if (CardType == CardType.Creature)
            {
                owner.board.AddCard(this);             
            }

            Events.Cards.Played?.Invoke(this);

            if (CardType == CardType.Spell)
            {
                owner.graveyard.AddCard(this);
            }

            if(CardType == CardType.Weapon)
            {
                owner.SetWeapon(this as Weapon);
                transform.position = owner.GetTransform().position + new Vector3(-2, 0, 0);
               //owner.graveyard.AddCard(this);
            }

            OnPlay();

            EventManager.Instance.CreaturePlayed.Raise(
                new ActionContext
                {
                    TargetEntity = target,
                    TriggerEntity = this
                });
            ExecuteOnPlayAction(target);
        }

        protected virtual void OnPlay() { }
        protected virtual void OnCardDestroyed() { }
        public bool IsInHand()=> zone.zoneType == ZoneType.Hand;
        public bool IsOnBoard()=> zone.zoneType == ZoneType.Board;
        public TargetFilter GetTargetFilter()
        {
            if (IsCreature() && IsOnBoard())
            {
                return new TargetFilter
                {
                    enemy = true,
                    creature = true,
                    player = true
                };
            }
            if (IsInHand())
            {
                return CardData.targetFilter;
            }

            return new TargetFilter();
        }
        public virtual bool IsDead()=> _isDead;
        public bool CanBeSelectedBy(Player player)=> owner == player && (IsInHand() || IsOnBoard());
        public bool CanBeHighlighted(Player player, Card selectedCard)
        {
            if(selectedCard == null)
            {
                return IsInHand() && owner == player || IsOnBoard();
            }

            if (this == selectedCard) return false;

            var filter = selectedCard.GetTargetFilter();
            return filter.Match(selectedCard, this);
        }
        public void Highlight(bool value)=> UI.Highlight(value);
        public bool IsCreature()=> CardType== CardType.Creature;
        public bool IsSpell()=> CardType == CardType.Spell;
        public bool IsWeapon() => CardType == CardType.Weapon;
        public bool IsPlayer() => false;
        public Player GetOwner() => owner;
        public Card GetCard() => this;
        public Transform GetTransform() => transform;
        public virtual void AddBuff(Buff buff) { }
        public virtual void RemoveBuff(Buff buff) { }
        public virtual List<Buff> GetBuffs() { return _buffs; }
        public virtual void Damage(int amount, bool triggerEvent, ITargetable source) { }
        public virtual void Heal(int amount, bool triggerEvent) { }
        public void SetHealth(int health, int maxHealth)=> UI.SetHealth(health, maxHealth);
        public virtual int GetAttack() => 0;
        public Player GetPlayer() => owner;
        public void AnimateDamage(Vector3 scale, float duration)=> UI.AnimateDamage(scale, duration);
        public virtual void Kill() { }
        public bool CanBeTargeted()=> IsOnBoard() && !IsDead();
        public virtual void TurnStarted() { }
        public virtual int GetCost() 
        {
            int totalCost = CardData.manaCost;

            foreach(Buff buff in _buffs)
            {
             
            }
            if (totalCost < 0) totalCost = 0;
            return totalCost; 
        }
        public virtual void ChangeCost(int amount)
        {
            //int totalCost = CardData.manaCost;
            //totalCost += modifiers;
            //Events.Cards.ChangedCost?.Invoke(this, manaCost);
        }

    }
}
