using UnityEngine;

public class CardVisual : MonoBehaviour
{
    public Card cardInfo;
    public Sprite sprite;

    public void UpdateCardVisual(Card _card)
    {
        cardInfo = _card;
        
        sprite = CardSpiriteManager.inst.GetSpriteFromCard(cardInfo);
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
