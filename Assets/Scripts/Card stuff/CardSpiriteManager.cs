using UnityEngine;

public class CardSpiriteManager : MonoBehaviour
{
    public static CardSpiriteManager inst;

    [SerializeField] public Sprite[] cardSprites;

    private void Awake()
    {
        if (inst != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        inst = this;
    }

    private void Start()
    {
        
    }

    public Sprite GetSpriteFromCard(Card _card)
    {
        return cardSprites[((int)_card.suit * 13) + (_card.value - 2)];
    }
}
