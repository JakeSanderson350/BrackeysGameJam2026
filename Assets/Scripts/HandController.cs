using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] protected Hand hand;

    private void Start()
    {
        //InitController();
    }

    public void InitController()
    {
        hand = GetComponent<Hand>();

        // Add hand to game manager
        GameManager.inst.AddHand(hand);
    }
}
