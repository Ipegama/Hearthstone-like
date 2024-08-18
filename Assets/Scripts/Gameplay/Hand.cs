using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Hand : MonoBehaviour
    {
        public List<Card> _cards = new List<Card>();

        private Player _owner;
        public void Initialize(Player owner)
        {
            _owner = owner;
        }
    }
}