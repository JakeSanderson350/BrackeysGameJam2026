using UnityEngine;

[CreateAssetMenu(fileName = "New Game Settings", menuName = "CardGameSettings")]
public class CardGameSettings : ScriptableObject
{
    public int numPlayers = 4;
    public int cardsToDeal = 2;
}
