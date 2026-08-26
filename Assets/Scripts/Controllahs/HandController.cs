using EditorAttributes;
using System;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // Id for checking if in turn
    private static int idCounter = 0;
    private int id;

    protected TurnType currentTurn;
    protected bool isInTurn = false;
    protected bool isFolded = false;

    [SerializeField] public Hand hand;
    [SerializeField] public int money;

    public event Action<HandController> OnFold;

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

    private void OnEnable()
    {
        GameManager.OnStartPlayerTurn += DoTurn;
    }

    private void OnDisable()
    {
        GameManager.OnStartPlayerTurn -= DoTurn;
    }

    public void InitController()
    {
        hand = GetComponent<Hand>();
        id = idCounter++;

        // Add hand to game manager
        GameManager.inst.AddHand(this);
    }

    public int GetID()
    {
        return id;
    }

    private void DoTurn(int _id, TurnType turnType)
    {
        if (_id != id)
            return;

        isInTurn = true;
        currentTurn = turnType;
    }

    // Bet Phase
    public void Raise(int _raise)
    {

    }

    public void Call()
    {
        Debug.Log(" fuckin ell");
    }

    public void Fold()
    {

    }
}
