using UnityEngine;
using UnityEngine.UI;

// Player controls
public class Player : HandController
{
    private void Update()
    {
        if (isInTurn)
        {
            switch(currentTurn)
            {
                case TurnType.ANTE:
                    // Turn on ante buttons

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
    }

    public void BitchPlease()
    {
        Debug.Log(" Ya bish");
    }
}
