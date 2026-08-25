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
        if (handVisual != null)
        {
            for (int i = 0; i < handVisual.Count; i++)
            {
                GameObject obj = handVisual[i];
                handVisual.Remove(obj);
                Destroy(obj);
            }
        }

        // Create new models and space accordingly
        handVisual = new List<GameObject>();

        for (int i = 0; i < _cards.Count; i++)
        {
            GameObject newCard = Instantiate(cardVisualBase, gameObject.transform);
            
            CardVisual cardVis = newCard.GetComponent<CardVisual>();
            cardVis.UpdateCardVisual(_cards[i]);

            Debug.Log((float)i / (float)_cards.Count);

            float cardX = Mathf.Lerp(-width / 2.0f, width / 2.0f, (float)i / (float)_cards.Count);
            float cardZ = Mathf.Lerp(0.0f, 1.0f, (float)i / (float)_cards.Count);

            cardVisualBase.transform.position = new Vector3(cardX, 0.0f, cardZ);

            handVisual.Add(newCard);
        }
    }
}
