using TriggerSystem;
using UnityEngine;

namespace Gameplay.Interfaces
{
    public interface ITargetable
    {
        bool CanBeTargeted();
        bool IsCreature();
        bool IsSpell();
        bool IsWeapon();
        bool IsPlayer();
        Player GetOwner();
        Transform GetTransform();
        void AddBuff(Buff buff);


        void Damage(int amount, bool triggerEvent, ITargetable source);
        void Heal(int amount, bool triggerEvent);
        void SetHealth(int health, int maxHealth);
        int GetAttack();
        Player GetPlayer();
        void AnimateDamage(Vector3 scale, float duration);
        void Kill();
    }
}