using Data;
using DG.Tweening;
using GameAnimations;
using Gameplay.Interfaces;
using System;
using System.Collections.Generic;
using TriggerSystem;
using UI;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class Player : MonoBehaviour, ITargetable
    {
        public StartingDeckData startingDeckData;

        [SerializeField] private Zone deck;
        [SerializeField] private Zone hand;
        [SerializeField] public Zone graveyard;
        [SerializeField] public Zone board;

        public PlayerStatsUI playerStats;

        public event Action<int, int> ManaChanged;

        private int _mana;
        private int _maximumMana;

        private int _health;
        private int _maxHealth;

        public void Initialize(int startingMana, int health)
        {
            deck.Initialize(this);
            hand.Initialize(this);
            graveyard.Initialize(this);
            board.Initialize(this);

            _maximumMana = startingMana;
            SetMana(_maximumMana);

            _maxHealth = health;
            _health = _maxHealth;

            InitializeDeck();

            Events.Turns.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStart(Player player)
        {
            if(player == this)
            {
                board.TurnStarted();
                SetMana(_maximumMana);
                DrawCard();
            }
        }

        private void SetMana(int mana)
        {
            _mana = mana;
            Events.Players.ManaChanged?.Invoke(this, mana, _mana, _maximumMana);
        }

        private void InitializeDeck()
        {
            foreach(var cardData in startingDeckData.GetCards())
            {
                var card = cardData.Create(this);
                deck.AddCard(card);
            }
            deck.Shuffle();
        }

        private void OnEnable()
        {
            Events.Resolve += OnResolve;
        }

        private void OnResolve()
        {
            foreach(Creature creature in board.GetCreatures())
            {
                if (creature.IsDead())
                {
                    OnCratureDeath(creature);
                }
            }
        }
        private void OnCreatureDeath(Creature creature)
        {
            if(creature.owner == this)
            {
                creature.OnCreatureDeath();
                graveyard.AddCard(creature);
            }
        }

        public void DrawCard()
        {
            var card = deck.GetFirstCard();
            if(card != null)
            {
                hand.AddCard(card);
                Events.Cards.Drawn?.Invoke(card);
            }
        }

        public void PlayCard(Card card, ITargetable target = null)
        {
            if (card == null) return;

            var cost = card.CardData.manaCost;
            if (_mana < cost) return;
            _mana -= cost;
            Events.Players.ManaChanged?.Invoke(this, cost, _mana, _maximumMana);

            AnimationsQueue.Instance.StartQueue();

            card.Play(target);

            Events.Resolve?.Invoke();
            AnimationsQueue.Instance.EndQueue();
        }

        public void StartAttack(Creature card, ITargetable target)
        {
            AnimationsQueue.Instance.StartQueue();

            card.Attack(target);

            Events.Resolve?.Invoke();
            AnimationsQueue.Instance.EndQueue();
        }

        public Card GetRandomLivingCreature()
        {
            var livingCreature = new List<Creature>();
            foreach(var card in board.GetCreatures())
            {
                if (!card.IsDead())
                {
                    livingCreature.Add(card);
                }
                return livingCreature.Random();
            }
        }

        public void DoAction(Card card,ITargetable target)
        {
            if(card.IsInHand())
            {
                PlayCard(card, target);
            }
            else if (card.IsOnBoard())
            {
                if(card is Creature creature)
                {
                    StartAttack(creature, target);
                }
            }
        }
        public void DoAction(Card card, Zone zone)
        {
            if(card.IsInHand() && zone.zoneType == ZoneType.Board)
            {
                PlayCard(card);
            }
        }
        public bool CanBeTargeted() => false;
        public bool IsCreature()=> false;
        public bool IsSpell()=> false;
        public bool IsPlayer()=>false;
        public Player Owner() => this;
        public Transform GetTransform() => playerStats.transform;
        public void AddBuff(Buff buff) { }
        public void Damage(int amount,bool triggerEvent, ITargetable source)
        {
            _health-=amount;
            Events.Creatures.Damaged?.Invoke(this, amount, _health, _maxHealth);

            if (triggerEvent)
            {
                EventManager.Instance.CreatureDamaged.Raise(
                new ActionContext
                {
                    TriggerEntity = this,
                    DamagingEntity = source,
                    EventAmount = amount,
                });
            }
        }
        public void Heal(int amount,bool triggerEvent)
        {
            if (_health + amount > _maxHealth)
            {
                amount= _maxHealth-_health;
            }
            _health+=amount;
            Events.Creatures.Healed?.Invoke(this,amount, _health, _maxHealth);

            if (triggerEvent)
            {
                EventManager.Instance.CreatureHealed.Raise(
              new ActionContext
              {
                  TriggerEntity = this,
              });
            }
        }
        public void SetHealth(int health,int maxHealth)=> playerStats.SetHealth(health, maxHealth);
        public int GetAttack() => 0;
        public Player GetPlayer() => this;
        public void AnimateDamage(Vector3 scale, float duration)
        {
            var tf = GetTransform();
            tf.DOComplete();
            tf.DOPunchScale(scale,duration);
        }
        public void Kill() => _isDead = true;

        public bool IsDead() => _health <= 0;

        public List<ITargetable> GetAllTargets(Card card,TargetFilter filter)
        {
            var result = new List<ITargetable>();
            if (filter.Match(card, this) && !IsDead()) result.Add(this);

            foreach(var creature in board.GetCreatures())
            {
                if (filter.Match(card, creature) && !creature.IsDead()) result.Add(creature);
            }
            return result;
        }
        public void ChangeMana(int currentMana, int maximumMana)
        {
            if (currentMana > 0)
            {
                _mana += currentMana;
                Events.Players.ManaChanged?.Invoke(this,currentMana,_mana,_maximumMana);
            }

            if(maximumMana > 0)
            {
                _maximumMana += maximumMana;
                Events.Players.MaxManaChanged?.Invoke(this,currentMana, _mana,_maximumMana);
            }
        }
    }
}