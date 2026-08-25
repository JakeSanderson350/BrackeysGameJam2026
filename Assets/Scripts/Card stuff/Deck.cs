using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> deck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void InitDeck()
    {
        deck = new List<Card>();

        InitStandardDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card DrawCard()
    {
        int ind = Random.Range(0, deck.Count);

        Card cardToReturn = deck[ind];
        deck.Remove(cardToReturn);

        return cardToReturn;
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
