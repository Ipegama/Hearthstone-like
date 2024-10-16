using Gameplay.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName ="Bucket/Starting Deck")]
    public class StartingDeckData : ScriptableObject
    {
        public List<CardData> cards;

        public List<CardData> GetCards()
        {
            var list = new List<CardData>(cards);
            return list;
        }
    }
}