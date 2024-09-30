using Gameplay;
using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TriggerSystem;
using TriggerSystem.Data;
using UnityEngine;

namespace GameAnimations
{
    public class AnimationsQueue : MonoBehaviour
    {
        public static AnimationsQueue Instance;

        private Queue<GameAnimationQueue> _queue = new Queue<GameAnimationQueue>();
        private GameAnimationQueue _lastQueue;

        public DamageCanvas damageCanvasPrefab;
        public DamageCanvas healCanvasPrefab;
        private Coroutine _coroutine;

        public void StartQueue()
        {
            _lastQueue = new GameAnimationQueue();
        }

        public void EndQueue()
        {
            if(_lastQueue == null) return;
            
            _queue.Enqueue(_lastQueue);

            if(_coroutine == null)
            {
                _coroutine = StartCoroutine(ExecuteQueueCoroutine());
            }
            _lastQueue = null;
        }

        private void Awake()
        {
            Instance = this;

            Events.Zones.CardAdded += OnZoneCardAdded;
            Events.Zones.CardRemoved += OnZoneCardRemoved;

            Events.Actions.Projectile += OnProjectile;

            Events.Creatures.Damaged += OnCreatureDamaged;
            Events.Creatures.Healed += OnCreatureHealed;
            Events.Creatures.AttackChanged += OnAttackChanged;
            Events.Creatures.Attack += OnAttack;
            Events.Creatures.MaxHealthChanged += OnMaxHealthChanged;

            Events.Cards.Triggered += OnCardTriggered;
            Events.Cards.Created += OnCardCreated;
            Events.Cards.ChangedCost += OnCardChangedCost;

            Events.Resolve += OnResolve;

            Events.Players.ManaChanged += OnManaChanged;
            Events.Players.MaxManaChanged += OnMaxManaChanged;
        }
        private void OnCardChangedCost(Card card,int amount)
        {
            Enqueue(new ManaCostChangedAnimation(card,amount));
        }
        private void OnMaxHealthChanged(ITargetable target, int amound, int health, int maxHealth)
        {
            Enqueue(new MaxHealthChangedAnimation(target, amound, health, maxHealth));
        }
        private void OnMaxManaChanged(Player player,int amount,int current, int maximum)
        {
            Enqueue(new ManaChangedAnimation(player,amount,current,maximum));
        }
        private void OnManaChanged(Player player, int amount, int current, int maximum)
        {
            Enqueue(new ManaChangedAnimation(player, amount, current, maximum));
        }
        private void OnCardCreated(Card card)
        {
            Enqueue(new CardCreatedAnimation(card));
        }
        private void OnResolve()
        {
            Enqueue(new DelayAnimation(1f));
        }
        private void OnAttack(Card source, ITargetable target)
        {
            Enqueue(new AttackAnimation(source,target));
        }
        private void OnCardTriggered(Card card)
        {
            Enqueue(new CardTriggeredAnimation(card));
        }
        private void OnAttackChanged(Card card, int amount, int attack)
        {
            Enqueue(new AttackChangedAnimation(card,amount,attack));
        }
        private void OnCreatureDamaged(ITargetable target,int damageAmount,int health, int maxHealth)
        {
            Enqueue(new DamageAnimation(target,damageAmount,health,maxHealth));
        }
        private void OnCreatureHealed(ITargetable target, int healedAmount, int health, int maxHealth)
        {
            Enqueue(new HealAnimation(target, healedAmount, health, maxHealth));
        }
        private void OnProjectile(ProjectileActionData data, Card source, Transform sourcePos, Transform target)
        {
            Enqueue(new ProjectileAnimation(data,source,sourcePos,target));
        }
        private void OnZoneCardRemoved(Zone zone,  List<Card> cards, Card card)
        {
            Enqueue(new ZoneChangedAnimation(zone,cards,card,0f));
        }
        private void OnZoneCardAdded(Zone zone, List<Card> cards, Card card)
        {
            Enqueue(new ZoneChangedAnimation(zone,cards,card,0.4f));
        }
        private void Enqueue(GameAnimation anim)
        {
            if(_lastQueue != null)
            {
                _lastQueue.Queue.Enqueue(anim);
            }
            else
            {
                anim.ExecuteWithoutAnimation();
            }
        }
        private IEnumerator ExecuteQueueCoroutine()
        {
            while(_queue.Count > 0)
            {
                var currentQueue = _queue.Dequeue();
                yield return currentQueue.Execute();
            }
            _coroutine = null;
        }
    }

    public class GameAnimationQueue 
    {
        public Queue<GameAnimation> Queue;

        public GameAnimationQueue()
        {
            Queue = new Queue<GameAnimation>();
        }

        public IEnumerator Execute()
        {
            while(Queue.Count > 0)
            {
                var currentAnimation = Queue.Dequeue();

                bool shouldStop = false;
                shouldStop = currentAnimation is DelayAnimation && Queue.Count == 0;
                if (!shouldStop)
                {
                    yield return currentAnimation.Execute();
                }
            }
        }
    }
}
