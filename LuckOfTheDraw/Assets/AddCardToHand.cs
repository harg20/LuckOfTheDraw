using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCardToHand : MonoBehaviour
{
    public Card card;
    public GenerateHand handgen;
    // Start is called before the first frame update
    void Start()
    {
        handgen = FindObjectOfType<GenerateHand>();
        //card.image = GetComponent<Image>().sprite;
    }
    public void AddToHand()
    {
        handgen.AddCardToHand(card);
    }

    // Update is called once per frame
   
}
