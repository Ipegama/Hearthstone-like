using System;
using Gameplay;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Animations/Projectile")]
    public class ProjectileActionData : ScriptableObject
    {
        public Projectile projectilePrefab;

        public void Execute(Card card, Transform target, Action callback)
        {
            var sourcePos = card.transform.position;

            var proj = Instantiate(projectilePrefab,sourcePos,Quaternion.identity);
            proj.LaunchAt(target);
            proj.Triggered += callback;
        }
    }
}