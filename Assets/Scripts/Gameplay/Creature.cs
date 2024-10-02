using System.Collections.Generic;
using System.Linq;
using Gameplay.Data;
using Gameplay.Interfaces;
using TriggerSystem;

namespace Gameplay
{
    public class Creature : Card
    {
        public CreatureData _creatureData;

        private int _health;
        private int _maxHealth;

        private int _attack;
        private bool _canAttack;
        private bool _isFrozen;

        private List<GameTrigger> _gameTriggers = new List<GameTrigger>();

        public override void SetData(CardData data)
        {
            base.SetData(data);
            CardType = CardType.Creature;

            _creatureData = data as CreatureData;

            _maxHealth = _creatureData.maxHealth;
            _health = _maxHealth;
            _attack = _creatureData.attack;
            _canAttack = false;
        }

        public void Attack(ITargetable target)
        {
            if (!_canAttack) return;
            if (_attack <= 0) return;

            _canAttack = false;
            Events.Creatures.Attack?.Invoke(this, target);

            target.Damage(_attack, false, this);
            Damage(target.GetAttack(), false, target);

            EventManager.Instance.CreatureDamaged.Raise(
                new ActionContext
                {
                    TriggerEntity = target,
                    DamagingEntity = this,
                    EventAmount = _attack,
                });

            EventManager.Instance.CreatureDamaged.Raise(
                new ActionContext
                {
                    TriggerEntity = this,
                    DamagingEntity = target,
                    EventAmount = target.GetAttack(),
                });
        }

        public override void Damage(int amount, bool triggerEvent, ITargetable source)
        {
            _health -= amount;
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

        public override void Heal(int amount, bool triggerEvent)
        {
            if (_health + amount > _maxHealth)
            {
                amount = _maxHealth - _health;
            }
            _health += amount;

            Events.Creatures.Healed?.Invoke(this, amount, _health, _maxHealth);

            if (triggerEvent)
            {

                EventManager.Instance.CreatureDamaged.Raise(
                    new ActionContext
                    {
                        TriggerEntity = this,
                    });
            }
        }

        public override void Kill()
        {
            _isDead = true;
        }
        public override int GetAttack()
        {
            return _attack;
        }

        public void ChangedAttack(int amount)
        {
            _attack += amount;
            Events.Creatures.AttackChanged?.Invoke(this, amount, _attack);
        }

        public void ChangedMaxHealth(int amount)
        {
            _maxHealth += amount;
            _health += amount;
            Events.Creatures.MaxHealthChanged?.Invoke(this, amount, _health, _maxHealth);
        }

        protected override void OnPlay()
        {
            RegisterTriggers();
        }

        protected override void OnCardDestroyed()
        {
            UnregisterTriggers();
        }

        public override void AddBuff(Buff buff)
        {
            _buffs.Add(buff);
            buff.OnApply(this);
        }
        public override void RemoveBuff(Buff buff)
        {
            _buffs.Remove(buff);
            buff.OnRemove(this);
        }

        public void ClearBuffs()
        {
            foreach (var buff in _buffs)
            {
                buff.OnRemove(this);
            }
            _buffs.Clear();
        }

        public void Freeze()
        {
            _isFrozen = true;
            _canAttack = false;
            UI.SetFreeze(_isFrozen);
            Events.Creatures.Frozen?.Invoke(this);
        }


        public void SetTaunt()
        {
            foreach (var buff in _buffs)
            {
                if (buff is TauntBuff taunt)
                {
                    UI.SetTaunt(true);
                    return;
                }
            }
            UI.SetTaunt(false);
        }

        public void OnCreatureDeath()
        {
            EventManager.Instance.CreatureDeath.Raise(
                new ActionContext
                {
                    TriggerEntity = this,
                    TargetEntity = null,
                });
            UnregisterTriggers();
        }

        private void RegisterTriggers()
        {
            foreach (var trigger in _creatureData.triggers)
            {
                _gameTriggers.Add(new GameTrigger(trigger, this));
            }

            foreach (var trigger in _gameTriggers)
            {
                trigger.gameEvent.AddTrigger(trigger);
            }
        }

        private void UnregisterTriggers()
        {
            foreach (var trigger in _gameTriggers)
            {
                trigger.gameEvent.RemoveTrigger(trigger);
            }
        }
        public override bool IsDead()
        {
            return base.IsDead() || _health <= 0;
        }

        public override void TurnStarted()
        {
            if (_isFrozen) _isFrozen = false;
            else _canAttack = true;

            UI.SetFreeze(_isFrozen);
        }
        public int GetSpellpower()=> _buffs.OfType<SpellpowerBuff>().Sum(buff => buff.GetSpellpower());
        public bool HasTaunt()=> _buffs.Any(buff => buff is TauntBuff);

    }
}