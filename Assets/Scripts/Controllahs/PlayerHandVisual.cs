using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandVisual : MonoBehaviour
{
    [SerializeField] private Hand playerHand;
    [SerializeField] private GameObject cardVisualBase;

    [Header("Settings")]
    [SerializeField] private float width;

    private List<GameObject> handVisual;

    private void OnEnable()
    {
        if (playerHand == null)
            return;

        playerHand.OnHandChanged += UpdateVisual;

        handVisual = new List<GameObject>();
    }

    private void OnDisable()
    {
        if (playerHand == null)
            return;

        playerHand.OnHandChanged -= UpdateVisual;
    }

    private void UpdateVisual(List<Card> _cards)
    {
        // Delete old models
        for (int i = 0; i < handVisual.Count; i++)
        {
            GameObject obj = handVisual[i];
            Destroy(obj);
        }
        handVisual.Clear();        

        // Create new models and space accordingly
        handVisual = new List<GameObject>();

        for (int i = 0; i < _cards.Count; i++)
        {
            GameObject newCard = Instantiate(cardVisualBase, gameObject.transform);
            
            CardVisual cardVis = newCard.GetComponent<CardVisual>();
            cardVis.UpdateCardVisual(_cards[i]);

            float cardX = Mathf.Lerp(-width / 2.0f, width / 2.0f, (float)i / (float)_cards.Count);
            float cardZ = Mathf.Lerp(1.0f, 0.0f, (float)i / (float)_cards.Count);

            cardVis.transform.position = new Vector3(cardX, cardVis.transform.position.y, cardZ);

            handVisual.Add(newCard);
        }
    }
}
