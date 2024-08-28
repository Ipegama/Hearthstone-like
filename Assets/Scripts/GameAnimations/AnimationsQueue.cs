using Gameplay;
using System.Collections;
using System.Collections.Generic;
using TriggerSystem;
using UnityEngine;

namespace GameAnimations
{
    public class AnimationsQueue : MonoBehaviour
    {
        public static AnimationsQueue Instance;

        public static bool AnimationsEnabled = true;

        private Queue<GameAnimation> _queue = new Queue<GameAnimation>();

        public DamageCanvas damageCanvasPrefab;
        private Coroutine _coroutine;

        private void Awake()
        {
            Instance = this;
        }
        private void OnEnable()
        {
            Events.Zones.CardAdded += OnZoneCardAdded;
            Events.Zones.CardRemoved += OnZoneCardRemoved;
            Events.Actions.Projectile += OnProjectile;
            Events.Creatures.Damaged += OnCreatureDamaged;
        }
        private void OnCreatureDamaged(Card card, int damageAmount, int health, int maxHealth) 
        {
            Enqueue(new DamageAnimation(card,damageAmount, health, maxHealth)); 
        }
        private void OnProjectile(ProjectileActionData data, ActionContext context) 
        {
            Enqueue(new ProjectileAnimation(data,context));
        }
        private void OnZoneCardRemoved(Zone zone, List<Card> cards, Card card) 
        {
            Enqueue(new ZoneChangedAnimation(zone,cards,card,0f));
        }
        private void OnZoneCardAdded(Zone zone, List<Card> cards,Card card)
        {
            Enqueue(new ZoneChangedAnimation(zone, cards, card, 0.4f));
        }

        private void Enqueue(GameAnimation animation)
        {
            _queue.Enqueue(animation);
            ExecuteQueue();
        }

        private void ExecuteQueue()
        {
            if(_coroutine == null) 
            {
                _coroutine = StartCoroutine(ExecuteQueueCoroutine());
            }
        }

        private IEnumerator ExecuteQueueCoroutine()
        {
            while(_queue.Count > 0)
            {
                var currentAnimation = _queue.Dequeue();
                if (AnimationsEnabled)
                {
                    yield return currentAnimation.Execute();
                }
                else
                {
                    currentAnimation.ExecuteWithoutAnimation();
                }

                _coroutine = null;
            }
        }
    }
}
