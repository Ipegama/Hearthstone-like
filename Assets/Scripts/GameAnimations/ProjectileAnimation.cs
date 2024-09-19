using Gameplay;
using System.Collections;
using TriggerSystem;
using TriggerSystem.Data;
using UnityEngine;

namespace GameAnimations
{
    internal class ProjectileAnimation : GameAnimation
    {
        private ProjectileActionData _data;
        private Card _source;
        private Transform _sourceTransform;
        private Transform _target;

        private bool _projectileTriggered;

        public ProjectileAnimation(ProjectileActionData data, Card source, Transform sourceTransform, Transform target)
        {
            _data = data;
            _source = source;
            _target = target;
            _sourceTransform = sourceTransform;
        }

        public override IEnumerator Execute()
        {
            _projectileTriggered = false;
            _data.Execute(_source, _sourceTransform, _target, OnProjectileTriggered);

            yield return new WaitUntil(() => _projectileTriggered);
        }

        public override void ExecuteWithoutAnimation()
        {
            
        }

        private void OnProjectileTriggered()
        {
            _projectileTriggered = true;
        }
    }
}