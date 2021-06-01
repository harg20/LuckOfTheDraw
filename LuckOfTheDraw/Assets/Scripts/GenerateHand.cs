using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHand : MonoBehaviour
{
    public List<Card> cards;
    public GameObject cardTemplate;
    public List<GameObject> hand;
    public CardDataTracker cardData;
    public int handSize = 6;
    public float offset = 50;
    public float cardscale = .75f;
 
    
    // Start is called before the first frame update
    void Start()
    {
        cards = cardData.rarityScaledList;
        NewHand();
        //cardData.restart();
    }
 
    public void NewHand()
    {
        cards = cardData.rarityScaledList;
       
        for (int i = hand.Count; i < handSize; i++)
        {
            
            int randcard = Random.Range(0, cards.Count);

        
           
            GameObject handcard = Instantiate(cardTemplate, transform);
            hand.Add(handcard);
            
            handcard.transform.localScale = new Vector3(cardscale, cardscale, cardscale);
            handcard.GetComponent<CardDisplay>().card = cards[randcard];
            handcard.name  = handcard.GetComponent<CardDisplay>().card.name + ", " + i;
            handcard.transform.position = new Vector3((i + 1) * offset, transform.position.y, 0);
           
        }

    }

    public void RemoveCard()
    {
        hand[0].GetComponent<CardDisplay>().card.ResetInherited();
        Destroy(hand[0]);
        hand.Remove(hand[0]);
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.position = new Vector3((i + 1) * offset, transform.position.y, 0);
          
        }

    }
    

   public void Reorder(GameObject card)
    {
        if (hand.Count > 1)
        {


            //Debug.Log("foundgrabbedcard");
            for (int i = 0; i < hand.Count; i++)
            {

                if (card == hand[i])
                {
                    hand.Remove(hand[i]);

                }

            }
            for (int i = 0; i < hand.Count; i++)
            {

                if (card.transform.position.x < hand[i].transform.position.x && hand.Count < handSize)
                {

                    hand.Insert(i, card);
                    break;
                }
              




            }
            if (card.transform.position.x > hand[hand.Count - 1].transform.position.x && hand.Count < handSize)
            {
                hand.Add(card);
                
            }
           
        }
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.position = new Vector3((i + 1) * offset, transform.position.y, 0);
        }
    }
   
}
