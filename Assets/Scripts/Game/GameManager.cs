using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    private Player player;
    private TurnType gameTurn;

    [SerializeField] private CardGameSettings gameSettings;
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
        //DealHands();
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
        for (int i = 0; i < gameSettings.numPlayers - 1; i++)
        {
            GameObject newOpponent = Instantiate(opponentPrefab);
            newOpponent.GetComponent<Opponent>().InitController();

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
