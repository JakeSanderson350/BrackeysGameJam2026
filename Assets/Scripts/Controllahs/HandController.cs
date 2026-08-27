using EditorAttributes;
using System;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // Id for checking if in turn
    private static int idCounter = 0;
    [SerializeField] protected int id;

    protected TurnType currentTurn;
    protected bool isInTurn = false;
    protected bool isFolded = false;

    [SerializeField] public Hand hand;
    [SerializeField] public int money;

    public static event Action<HandController> OnCall;
    public static event Action<HandController, int> OnRaise; // int = amount raised
    public static event Action<HandController> OnPass; // Used when player is out

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
        money = GameManager.inst.gameSettings.startingCash;

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
        {
            isInTurn = false;
            return;
        }

        isInTurn = true;
        currentTurn = turnType;
    }

    // Bet Phase
    public void Raise(int _raise)
    {
        if ((money - _raise) < 0)
        {
            // Some sorta feedback to say the player cant raise
            return;
        }

        int amountRaised = Math.Min(_raise, GameManager.inst.GetCurrentMaxRaise());

        money = Math.Max(0, money - amountRaised);
        isInTurn = false;

        OnRaise?.Invoke(this, amountRaised);
    }

    public void Call()
    {
        if (money <= 0)
        {
            // Some sorta feedback to say the player cant call
            Debug.Log("No money :(");
            return;
        }
        else if ((money - GameManager.inst.GetCurrentBet()) < 0 && (money - GameManager.inst.GetCurrentBet()) > -GameManager.inst.GetCurrentBet())
        {
            // All in case
            Debug.Log("Shuuuvvvv");
        }

        money = Math.Max(0, money - GameManager.inst.GetCurrentBet());
        isInTurn = false;

        OnCall?.Invoke(this);
    }

    public void Fold()
    {

    }

    public void Pass()
    {
        OnPass?.Invoke(this);
    }
}
