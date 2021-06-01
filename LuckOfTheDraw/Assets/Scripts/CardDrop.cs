using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrop : MonoBehaviour
{
    public GameObject cardPickup;
    
    public GenerateHand handmaker;
    public CardDataTracker tracker;
  
    // Start is called before the first frame update
    void Start()
    {
        handmaker = FindObjectOfType<GenerateHand>();
        tracker.restart();
        
        
    }

    private void OnApplicationQuit()
    {
        tracker.restart();
    }


    public void SpawnCard(GameObject dropenemy)
    {
        if (tracker.possibleCards.Count > 0)
        {
            int cardpicked = Random.Range(0, tracker.possibleCards.Count - 1);
            var card = Instantiate(cardPickup);
            card.transform.position = dropenemy.transform.position;
            card.GetComponentInChildren<CardDisplay>().card = tracker.possibleCards[cardpicked];
            tracker.possibleCards.Remove(tracker.possibleCards[cardpicked]);
        }
    }
    // Update is called once per frame
 
}
