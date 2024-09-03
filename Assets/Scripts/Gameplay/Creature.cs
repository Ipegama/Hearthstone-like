using System.Collections.Generic;
using Data;
using Gameplay.Interfaces;
using TriggerSystem;
using TriggerSystem.Data;

namespace Gameplay
{
    public class Creature : Card
    {
        public CreatureData _creatureData;

        private int _health;
        private int _maxHealth;

        private int _attack;
        private bool _canAttack;

        private List<Buff> _buffs = new List<Buff>();
        private List<GameTrigger> _gameTriggers = new List<GameTrigger>();

        public override void SetData(CardData data)
        {
            base.SetData(data);
            CardType = CardType.Creature;

            _creatureData = data as CreatureData;
            _maxHealth = _creatureData.maxHealth;
            _health = _maxHealth;
            _attack = _creatureData.attack;
            _canAttack = true;
        }

        public void Attack(ITargetable target)
        {
            if (!_canAttack) return;
            if (_attack <= 0) return;

            _canAttack = false;
            Events.Creatures.Attack?.Invoke(this, target);
            target.Damage(_attack, false,this);
            Damage(target.GetAttack, false,target);
            target.TriggerDamagedEvent();
            TriggerDamagedEvent(target);
        }

        public override void Damage(int amount, bool triggerEvent, ITargetable source)
        {
            _health -= amount;
            Events.Creatures.Damaged?.Invoke(this, amount, _health, _maxHealth);

            if (triggerEvent)
            {
                TriggerDamagedEvent(source);
            }
        }

        public override void Heal(int amount, bool triggerEvent)
        {
            if(_health + amount > _maxHealth)
            {
                amount = _maxHealth-_health;
            }
            _health += amount;

            Events.Creatures.Healed?.Invoke(this,amount,_health,_maxHealth);

            if (triggerEvent)
            {
                TriggerHealEvent();
            }
        }

        private void TriggerHealEvent()
        {
            EventManager.Instance.CreatureHealed.Raise(
                new ActionContext
                {
                    TriggerEntity = this,
                });                
        }
    }
}