using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tracker", menuName = "CardDataTracker")]
public class CardDataTracker : ScriptableObject
{
    // Start is called before the first frame update
    public Card[] allCards;
    public List<Card> currentCards;
    public List<Card> possibleCards;
    

    public void restart()
    {
        currentCards.Clear();
        currentCards.Add(allCards[1]);
        UpdatePossibleCards();
        Debug.Log("resetti");
    }
    public void pickupcard(Card card)
    {
        currentCards.Add(card);
       
    }
    public void UpdatePossibleCards()
    {
        for (int i = 0; i < allCards.Length; i++)
        {
            if (!currentCards.Contains(allCards[i]) && !possibleCards.Contains(allCards[i]))
            {
                possibleCards.Add(allCards[i]);
            }
        }
        
    }
}
