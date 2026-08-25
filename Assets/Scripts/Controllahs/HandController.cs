using EditorAttributes;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] protected Hand hand;

    [Header("Debug")]
    [SerializeField] int cardToDiscard;
    [Button] void DiscardCard() => hand.DiscardCard(cardToDiscard);

    [Button]
    void DrawCard()
    {
        hand.PickupCard(GameManager.inst.baseDeck.DrawCard());
    }

    private void Start()
    {
        //InitController();
    }

    public void InitController()
    {
        hand = GetComponent<Hand>();

        // Add hand to game manager
        GameManager.inst.AddHand(hand);
    }
}
