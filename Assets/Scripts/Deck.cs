using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> deck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Init()
    {
        deck = new List<Card>();

        InitStandardDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitStandardDeck()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 13; i++)
            {
                Card newCard;

                newCard.value = (i % 13) + 2;
                newCard.suit = (Suit)j;

                deck.Add(newCard);
        }
        }
        
    }
}
