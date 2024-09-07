using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class DelayAnimation : GameAnimation
    {
        private float _delay;

        public DelayAnimation(float delay)
        {
            _delay = delay;
        }

        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(_delay);
        }

        public override void ExecuteWithoutAnimation()
        {
            
        }
    }
}
