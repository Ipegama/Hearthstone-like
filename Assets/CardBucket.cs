using Gameplay.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bucket/Card Bucket")]
public class CardBucket : ScriptableObject
{
    public string BucketName;
    public GameClass BucketClass;
    public List<CardData> cards;
}

public enum GameClass
{
    Druid,
    Hunter,
    Mage,
    Paladin,
    Priest,
    Rogue,
    Shaman,
    Warlock,
    Warrior
}
