using DG.Tweening;
using Gameplay;
using System.Collections;
using UnityEngine;

namespace GameAnimations
{
    public class TurnStartedAnimation : GameAnimation
    {
        private Player _player;
        

        public TurnStartedAnimation(Player player)
        {
            _player = player;
        }
        public override IEnumerator Execute()
        {
            if (_player != GameManager.Instance.players[0]) yield break;

            GameObject turnStartedObj = AnimationsQueue.Instance.yourTurnObj;

            turnStartedObj.SetActive(true);
            turnStartedObj.transform.localScale = Vector3.zero;

            turnStartedObj.transform.DOScale(Vector3.one, 0.3f);

            yield return new WaitForSeconds(0.3f);

            yield return new WaitForSeconds(2f);

            turnStartedObj.transform.DOScale(Vector3.zero, 0.3f);

            yield return new WaitForSeconds(0.3f);

            turnStartedObj.SetActive(false);
        }

        public override void ExecuteWithoutAnimation()
        {
        }
    }
}