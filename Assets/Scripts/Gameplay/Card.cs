using System;
using Data;
using Gameplay.Interfaces;
using TriggerSystem;
using UnityEngine;

namespace Gameplay
{
    public class Card : MonoBehaviour, IHighlightable, ITargetable
    {
        public CardData CardData { get; protected set; }
        public CardType CardType { get; protected set; }

        [HideInInspector] public CardUI UI;
        [HideInInspector] public Player owner;
        [HideInInspector] public Zone zone;

        private bool _isDead;

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
        public virtual void SetData(CardData data)
        {
            CardData = data;
        }
        private void ExecuteOnPlayAction(ITargetable target)
        {
            if(target == null)
            {
                /*if (CardData.targetFilter.HasTarget())
                {
                    throw new Exception("Target needed")
                }*/
                foreach(var action in CardData.playActions)
                {
                   /* action.Execute(new ActionContext
                    {
                        TargetEntity = target,
                        thisCard = this,
                        TriggerEntity = this
                    });
                   */
                }
            }
        }

        /////
        ///
        
        public bool IsInHand()
        {
            throw new NotImplementedException();
        }

        private void OnCardDestroyed()
        {
            throw new NotImplementedException();
        }
    }
}
