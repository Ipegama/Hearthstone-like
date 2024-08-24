using Gameplay;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        public TMP_Text manaText;

        public Player player;

        public void OnEnable()
        {
            player.ManaChanged += UpdateManaText;
        }
        public void OnDisable()
        {
            player.ManaChanged -= UpdateManaText;
        }

        public void UpdateManaText(int mana, int maxMana)
        {
            manaText.text = $"{mana}/{maxMana}";
        }
    }
}