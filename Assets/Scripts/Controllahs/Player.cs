using UnityEngine;
using UnityEngine.UI;

// Player controls
public class Player : HandController
{
    [SerializeField] private GameObject anteButtons;
    [SerializeField] private GameObject dealerButons;

    private void Update()
    {
        if (isInTurn)
        {
            switch(currentTurn)
            {
                case TurnType.ANTE:
                    // Turn on ante buttons
                    anteButtons.SetActive(true);
                    dealerButons.SetActive(false);
                    break;

                case TurnType.DEAL:
                    // Turn on cheat buttons
                    anteButtons.SetActive(false);
                    dealerButons.SetActive(true);
                    break;

                case TurnType.FIRST_BETS:
                    // Turn on bet buttons

                    break;

                case TurnType.DISCARD:
                    // Turn on discard UI

                    break;

                case TurnType.SECOND_BETS:
                    // Turn on bet buttons

                    break;
            }
        }
        else
        {
            anteButtons.SetActive(false);
            dealerButons.SetActive(false);
        }
    }

    public void DealerCheatOne()
    {
        Debug.Log("Nuffin yet :retard emji:");
    }

    public void DealerCheatTwo()
    {
        Debug.Log("Nuffin yet :retard emji:");
    }

    public void BitchPlease()
    {
        Debug.Log(" Ya bish");
    }
}
