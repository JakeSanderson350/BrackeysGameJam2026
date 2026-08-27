using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public const int MAX_RAISE_MULT = 3;

    // Tracking players
    private Player player; // irl human being not ai generated
    private List<Opponent> opponents;
    [SerializeField] private TurnType gameTurn;
    [SerializeField] private int playerInTurn;

    // Pot
    [SerializeField] private int totalPot = 0;
    [SerializeField] private int currentBet;
    [SerializeField] private int currentMaxRaise;

    [SerializeField] public CardGameSettings gameSettings;
    [SerializeField] private List<HandController> handsInPlay;

    [Header("Game Objects")]
    [SerializeField] public Deck baseDeck;
    [SerializeField] private GameObject opponentPrefab;
    [SerializeField] private GameObject playerPrefab;

    public static event Action<int, TurnType> OnStartPlayerTurn; // playerID of player which turn it is, What type of turn it is

    private void Awake()
    {
        if (inst != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        inst = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitOpponents();
        InitPlayer();

        baseDeck.InitDeck();
        gameTurn = TurnType.ANTE;
        //Prolly start coroutine to wait for opps

        playerInTurn = 0;
        currentBet = gameSettings.ante;
        OnStartPlayerTurn?.Invoke(playerInTurn, gameTurn);
    }

    private void OnEnable()
    {
        HandController.OnCall += PlayerCall;
        HandController.OnRaise += PlayerRaise;
        HandController.OnPass += PlayerPass;
    }

    private void OnDisable()
    {
        HandController.OnCall -= PlayerCall;
        HandController.OnRaise -= PlayerRaise;
        HandController.OnPass -= PlayerPass;
    }

    public int GetCurrentBet()
    {
        return currentBet;
    }

    public int GetCurrentMaxRaise()
    {
        return currentMaxRaise;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHand(HandController _hand)
    {
        handsInPlay.Add(_hand);
    }

    private void InitOpponents()
    {
        opponents = new List<Opponent>();

        for (int i = 0; i < gameSettings.numPlayers - 1; i++)
        {
            GameObject newOpponent = Instantiate(opponentPrefab);
            newOpponent.GetComponent<Opponent>().InitController();
            opponents.Add(newOpponent.GetComponent<Opponent>());

            // TODO
            // something that spawns them in spaced out around the table
            // Use Spline perhaps?
            newOpponent.transform.position += Vector3.right * (i + 1) * 1.5f;
        }
    }

    private void InitPlayer()
    {
        player = Instantiate(playerPrefab).GetComponent<Player>();
        player.InitController();
    }

    private void StartNextTurnType()
    {
        switch (gameTurn)
        {
            case TurnType.DEAL:
                DealHands();
                break;

            case TurnType.FIRST_BETS:
                currentBet = gameSettings.firstBetMin;
                currentMaxRaise = MAX_RAISE_MULT * currentBet;
                break;

            case TurnType.DISCARD:

                break;

            case TurnType.SECOND_BETS:

                break;
        }
    }

    private void DealHands()
    {
        for (int i = 0; i < handsInPlay.Count; i++)
        {
            for (int j = 0; j < gameSettings.cardsToDeal; j++)
            {
                handsInPlay[i].hand.PickupCard(baseDeck.DrawCard());
            }
        }
    }

    private void PlayerCall(HandController player)
    {
        totalPot += currentBet;
        
        IteratePlayerInTurn();
        OnStartPlayerTurn?.Invoke(playerInTurn, gameTurn);
    }

    private void PlayerRaise(HandController player, int raiseAmount)
    {
        totalPot += raiseAmount;
        currentBet = raiseAmount;
        
        IteratePlayerInTurn();
        OnStartPlayerTurn?.Invoke(playerInTurn, gameTurn);
    }

    private void PlayerPass(HandController player)
    {
        IteratePlayerInTurn();

        OnStartPlayerTurn?.Invoke(playerInTurn, gameTurn);
    }

    private void IteratePlayerInTurn()
    {
        playerInTurn++;

        if (playerInTurn > gameSettings.numPlayers - 1)
        {
            gameTurn++;
            playerInTurn = 0;
            StartNextTurnType();
        }
    }
}

public enum TurnType
{
    ANTE = 0,
    DEAL,
    FIRST_BETS,
    DISCARD,
    SECOND_BETS,
    SHOWDOWN
}
