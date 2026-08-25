using System.Collections.Generic;
using UnityEngine;

// Has cards in hand and controls actions
public class Hand : MonoBehaviour
{
    [SerializeField] private List<Card> hand;

    public void PickupCard(Card _card)
    {
        hand.Add(_card);
    }

    public void DiscardCard(int ind)
    {
        if (ind >= hand.Count)
            return;

        hand.RemoveAt(ind);
    }
}
