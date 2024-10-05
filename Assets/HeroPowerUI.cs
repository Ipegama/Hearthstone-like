using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroPowerUI : MonoBehaviour
{
    public TMP_Text manaCostText;
    public Image heroPowerSpriteImage;

    private HeroPower _heroPower;

    public void SetHeroPower(HeroPower heroPower)
    {
        _heroPower = heroPower;
    }
    private void UpdateUI()
    {
        heroPowerSpriteImage.sprite = _heroPower.Data.powerIcon;
    }
}
