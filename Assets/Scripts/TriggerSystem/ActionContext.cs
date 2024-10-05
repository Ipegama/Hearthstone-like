using System;
using System.Collections.Generic;
using Gameplay;
using Gameplay.Interfaces;

namespace TriggerSystem
{
    [Serializable]
    public struct ActionContext
    {
        public Card thisCard;
        public ITargetable TargetEntity;
        public ITargetable TriggerEntity;

        public ITargetable DamagingEntity;
        public int EventAmount;

        public ITargetable Get(TargetType type)
        {
            switch (type)
            {
                case TargetType.TargetEntity:
                    return TargetEntity;
                case TargetType.TriggeringEntity:
                    return TriggerEntity;        
                case TargetType.This:
                    return thisCard;
                case TargetType.TriggeringOwner:
                    return TriggerEntity.GetOwner();
                case TargetType.DamagingEntity:
                    return DamagingEntity;
                case TargetType.RandomEnemy:
                    return GameManager.Instance.GetEnemyOf(thisCard.owner).GetRandomLivingEnemy();
                case TargetType.RandomEnemyCreature:
                    return GameManager.Instance.GetEnemyOf(thisCard.owner).GetRandomLivingCreature();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type),type,null);
            }
        }

        public Player Get(TargetPlayerType type)
        {
            switch(type)
            {
                case TargetPlayerType.TriggeringPlayer:
                    return TriggerEntity.GetPlayer();
                case TargetPlayerType.Owner:
                    return thisCard.owner;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type),type,null);
            }
        }

        public List<ITargetable> GetAll(TargetType type, TargetFilter filter)
        {
            switch(type)
            {
                case TargetType.MatchingEntity:
                    return GameManager.Instance.GetAllEntities(thisCard, filter);
                default:
                    return new List<ITargetable> { Get(type) };
            }
        }

        public Dictionary<string, int> GetAllIntKeys()
        {
            var dict = new Dictionary<string, int>
            {
                { "EventAmount", EventAmount }
            };
            return dict;
        }
    }

}

