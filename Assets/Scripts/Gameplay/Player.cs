using Data;
using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class Player : MonoBehaviour
    {
        public StartingDeckData startingDeckData;
        public Hand hand;
        public Deck deck;

        public void Initialize()
        {
            deck.Initialize(this,startingDeckData);
            hand.Initialize(this);
        }
    }
}