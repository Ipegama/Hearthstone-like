using Gameplay;
using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class ManaChangedAnimation : GameAnimation
    {
        private Player _player;
        private int _amount;
        private int _current;
        private int _maximum;

        public ManaChangedAnimation(Player player, int amount, int current, int maximum)
        {
            _player = player;
            _amount = amount;
            _current = current;
            _maximum = maximum;
        }

        public override IEnumerator Execute()
        {
            _player.playerStats.AnimateManaChange();
            _player.playerStats.UpdateManaText(_current,_maximum);
            yield return new WaitForSeconds(0.2f);
        }

        public override void ExecuteWithoutAnimation()
        {
            _player.playerStats.UpdateManaText(_current,_maximum);
        }
    }
}
