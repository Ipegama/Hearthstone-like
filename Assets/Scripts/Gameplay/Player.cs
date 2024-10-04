using Data;
using DG.Tweening;
using Extensions;
using GameAnimations;
using Gameplay.Data;
using Gameplay.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TriggerSystem;
using UI;
using UnityEngine;
using static Events;

namespace Gameplay
{
    [Serializable]
    public class Player : MonoBehaviour, ITargetable, IBuffable
    {
        public StartingDeckData startingDeckData;
        public HeroPowerData startingHeroPowerData;

        [SerializeField] public HeroPower heroPower;
        [SerializeField] private Zone deck;
        [SerializeField] public Zone hand;
        [SerializeField] public Zone graveyard;
        [SerializeField] public Zone board;

        public PlayerStatsUI playerStats;

        public event Action<int, int> ManaChanged;

        private int _mana;
        private int _maximumMana;

        private int _health;
        private int _maxHealth;
        private bool _isFrozen;

        private List<Buff> _buffs = new List<Buff>();

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
            InitializeHeroPower();

            Events.Players.TurnStarted += OnTurnStart;

        }

        private void OnTurnStart(Player player)
        {
            if(player == this)
            {
                board.TurnStarted();
                SetMana(_maximumMana);
                DrawCard();
                if (_isFrozen) _isFrozen = false;
                playerStats.SetFreeze(_isFrozen);
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

        private void InitializeHeroPower()
        {
            heroPower.SetData(startingHeroPowerData);
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
                    OnCreatureDeath(creature);
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
                EventManager.Instance.CardDrawn.Raise(
                    new ActionContext
                    {
                        thisCard = card,
                    });
            }
        }

        public void AddCardToHand(CardData cardData)
        {
            var card = cardData.Create(this);

            if (card != null)
            {
                hand.AddCard(card);
            }
        }

        public void PlayCard(Card card, ITargetable target = null)
        {
            if (card == null) return;

            var cost = card.GetCost();
            if (_mana < cost) return;
            _mana -= cost;
            Events.Players.ManaChanged?.Invoke(this, cost, _mana, _maximumMana);

            AnimationsQueue.Instance.StartQueue();

            card.Play(target);

            if (card.IsSpell())
            {
                EventManager.Instance.SpellPlayed.Raise(new ActionContext
                {
                    thisCard = card,
                    TriggerEntity = this
                });
            }

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
            }
            return livingCreature.Random();
        }
        public ITargetable GetRandomLivingEnemy()
        {
            var livingEnemy = new List<ITargetable>() { this };

            foreach (var card in board.GetCreatures())
            {
                if (!card.IsDead())
                {
                    livingEnemy.Add(card);
                }
            }
            return livingEnemy.Random();
        }

        public void DoAction(Card card, ITargetable target)
        {
            if (card.IsInHand())
            {
                PlayCard(card, target);
            }
            else if (card.IsOnBoard())
            {
                if (card is Creature creature)
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

        public void DoAction(HeroPower heroPower, ITargetable target)
        {
            if (heroPower == null) return;

            var cost = heroPower.Data.manaCost;
            if (_mana < cost) return;
            _mana -= cost;
            Events.Players.ManaChanged?.Invoke(this, cost, _mana, _maximumMana);
            playerStats.UpdateManaText(_mana, _maximumMana);
            playerStats.AnimateManaChange();

            AnimationsQueue.Instance.StartQueue();

            heroPower.ExecuteOnPlayAction(target);

            Events.Resolve?.Invoke();
            AnimationsQueue.Instance.EndQueue();
        }
        public bool CanBeTargeted() => false;
        public bool IsCreature()=> false;
        public bool IsSpell()=> false;
        public bool IsPlayer()=>true;
        public Player GetOwner() => this;
        public Transform GetTransform() => playerStats.transform;
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
        public void Kill() { }

        public bool IsDead() => _health <= 0;

        public List<ITargetable> GetAllTargets(Card card,TargetFilter filter)
        {
            var result = new List<ITargetable>();

            if (filter.Match(card, this) && !IsDead()) 
            {
                result.Add(this);
            }

            foreach(var creature in board.GetCreatures())
            {
                if (filter.Match(card, creature) && !creature.IsDead())
                {
                    result.Add(creature);
                }
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

        public void Freeze()
        {
            _isFrozen = true;
            //_canAttack = false;
            playerStats.SetFreeze(_isFrozen);
            Events.Players.Frozen?.Invoke();
        }
        public int GetSpellpower()
        {
            return board.GetCreatures()
                .Where(c => !c.IsDead())
                .Sum(c => c.GetSpellpower());
        }

        public bool HasTaunt() => _buffs.Any(buff => buff is TauntBuff);

        public  void AddBuff(Buff buff)
        {
            _buffs.Add(buff);
            buff.OnApply(this);
        }
        public  void RemoveBuff(Buff buff)
        {
            _buffs.Remove(buff);
            buff.OnRemove(this);
        }

        public List<Buff> GetBuffs() => _buffs;
    }
}