using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        public TMP_Text manaText;
        public Transform manaParent;

        public TMP_Text healthText;

        public void UpdateManaText(int mana, int maxMana)
        {
            manaText.text = $"{mana}/{maxMana}";
        }

        public void SetHealth(int health, int maxHealth)
        {
            var color = Color.white;
            healthText.text = $"{health}";

            if (health < maxHealth) color = Color.red;

            healthText.color = color;
        }

        public void AnimateManaChange()
        {
            manaParent.DOComplete();
            manaParent.DOPunchScale(Vector3.one * 1.2f, 0.4f);
        }
    }
}