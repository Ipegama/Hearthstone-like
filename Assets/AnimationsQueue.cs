using Gameplay;
using System.Collections;
using System.Collections.Generic;
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
            //
            //
        }
        private void OnCreatureDamaged(Card card, int damageAmount, int health, int maxHealth) { }
       // private void OnProjectile(ProjectileActionData data, ActionContext context) { }
        private void OnZoneCardRemoved(Zone zone, List<Card> cards, Card card) { }
        private void OnZoneCardAdded(Zone zone, List<Card> cards,Card card)
        {
           // Enqueue(new Zone)
        }
        private void Enqueue(GameAnimation animation)
        {
            _queue.Enqueue(animation);

        }

        private void ExecureQueue()
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
