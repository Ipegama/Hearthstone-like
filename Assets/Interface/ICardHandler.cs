using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public interface ICardHandler
    {
        Vector3 GetPosition(Card card);
        Transform GetTransform();
    }
}