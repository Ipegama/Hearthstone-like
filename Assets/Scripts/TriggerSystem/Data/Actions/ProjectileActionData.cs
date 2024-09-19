using System;
using Gameplay;
using UnityEngine;

namespace TriggerSystem.Data
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Animations/Projectile")]
    public class ProjectileActionData : ScriptableObject
    {
        public Projectile projectilePrefab;

        public void Execute(Card card, Transform sourceTransform, Transform target, Action callback)
        {
            var proj = Instantiate(projectilePrefab,sourceTransform.position,Quaternion.identity);
            proj.LaunchAt(target);
            proj.Triggered += callback;
        }
    }
}