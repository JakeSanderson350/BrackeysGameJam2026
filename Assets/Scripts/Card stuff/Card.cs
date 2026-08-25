using JetBrains.Annotations;
using System;
using UnityEngine;

[Serializable] public struct Card
{
    [SerializeField] public int value;  // J = 11, Q = 12, K = 13, A = 14
    [SerializeField] public Suit suit;
}

public enum Suit
{
    CLUB = 0,
    DIAMOND,
    SPADE,
    HEART
}
