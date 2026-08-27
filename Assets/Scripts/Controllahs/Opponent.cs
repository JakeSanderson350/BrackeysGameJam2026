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
                    TryInfoCheat();
                    DoBet();
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

        Pass(); // passing cus no other action for bot to take during deal
    }

    private void TryInfoCheat()
    {
        // Shady shizzle
    }

    private void DoBet()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            Debug.Log("Chud call");
            Call();
        }
        else
        {
            // Get randopm raise amount between call val and max bet val
            int raiseAmount = (int)Mathf.Lerp((float)GameManager.inst.GetCurrentBet(), GameManager.inst.GetCurrentMaxRaise(), ((float)Random.Range(0, 100) / 100.0f));
            Debug.Log("Min click: " + raiseAmount);
            Raise(raiseAmount);
        }
    }
}
