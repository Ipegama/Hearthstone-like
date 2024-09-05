using Gameplay;
using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TriggerSystem;
using UnityEngine;

namespace GameAnimations
{
    public class AnimationsQueue : MonoBehaviour
    {
        public static AnimationsQueue Instance;

        private Queue<GameAnimationQueue> _queue = new Queue<GameAnimationQueue>();
        private GameAnimationQueue _lastQueue;

        public DamageCanvas damageCanvasPrefab;
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
            Events.Creatures.AttackChanged += OnAttackChanged;
            Events.Creatures.Attack += OnAttack;

            Events.Cards.Triggered += OnCardTriggered;
            Events.Cards.Created += OnCardCreated;

            Events.Resolve += OnResolve;
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
        private void OnCreatureHealed()
        {
        /////
        }
        private void OnProjectile(ProjectileActionData data, Card source, Transform target)
        {
            Enqueue(new ProjectileAnimation(data,source,target));
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
                _lastQueue.Enqueue(anim);
            }
            else
            {
                anim.ExecuteWithoutAnimation();
                ////
            }
        }
    }
}
