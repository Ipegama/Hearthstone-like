 using DG.Tweening;
using Gameplay;
using Gameplay.Interfaces;
using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class PlayerAttackAnimation : GameAnimation
    {
        private Player _source;
        private ITargetable _target;

        public PlayerAttackAnimation(Player source, ITargetable target)
        {
            _source = source;
            _target = target;
        }

        public override IEnumerator Execute()
        {

            var tf = _source.playerStats.heroPortrait.transform;

            tf.DOComplete();
            var defaultPosition = tf.position;

            tf.DOMoveY(2f,.5f); 
            yield return new WaitForSeconds(.5f);
            tf.DOMove(_target.GetTransform().position, .4f).SetEase(Ease.InBack);
            yield return new WaitForSeconds(0.4f);
            tf.DOMove(defaultPosition, .4f).SetEase(Ease.OutBack);
        }

        public override void ExecuteWithoutAnimation()
        {
        }
    }
}
