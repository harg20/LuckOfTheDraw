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
    public List<Card> rarityScaledList;


    public void restart()
    {
        rarityScaledList.Clear();
        currentCards.Clear();
        currentCards.Add(allCards[1]);
        UpdateScaledList(allCards[1]);
        UpdatePossibleCards();
        Debug.Log("resetti");
    }
    public void pickupcard(Card card)
    {
        currentCards.Add(card);
        UpdateScaledList(card);
        Debug.Log("Add");

    }
    public void UpdateScaledList(Card pickedupcard)
    {
        for (int i = 0; i < pickedupcard.rarity; i++)
        {
            rarityScaledList.Add(pickedupcard);
          
        }
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
