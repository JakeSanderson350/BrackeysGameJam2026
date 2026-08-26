using UnityEngine;

// Opponent decision logic prolly
public class Opponent : HandController
{
    private void Update()
    {
        if (isInTurn)
        {
            switch (currentTurn)
            {
                case TurnType.ANTE:
                    // Insta call
                    Call();
                    break;

                case TurnType.DEAL:
                    // Chance to try dealer cheat
                    TryDealerCheat();
                    break;

                case TurnType.FIRST_BETS:
                    // Chance to try info cheat

                    break;

                case TurnType.DISCARD:
                    // Chance to try physical cheat

                    break;

                case TurnType.SECOND_BETS:
                    // Chance to try emergency cheat

                    break;
            }
        }
    }

    public void TryDealerCheat()
    {
        // Do some shady shizz here

        Pass();
    }
}
