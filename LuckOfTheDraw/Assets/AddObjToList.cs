using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddObjToList : MonoBehaviour
{
    public GameObject itemtemplate;
    //public GameObject winitemtemplate;
    public GameObject content;
    public GenerateHand hand;
   // public GameObject wincontent;
    public List<GameObject> cards;
    
    
    // Start is called before the first frame update
 
    public void Start()
    {
        hand = FindObjectOfType<GenerateHand>();
        cards = hand.hand;
       //bots = GameObject.FindGameObjectsWithTag("bot");
        foreach (GameObject card in cards)
        {
            card.transform.parent = content.transform;
           // var copy = Instantiate(itemtemplate);
            //copy.transform.parent = content.transform;
            //copy.transform.GetChild(0).GetComponent<Text>().text = card.name;
           // copy.transform.GetChild(1).GetComponent<Text>().text = bot.GetComponent<BotStats>().cost.ToString();

            //var copywin = Instantiate(winitemtemplate);
            //copywin.transform.parent = wincontent.transform;
          //  copywin.transform.GetChild(0).GetComponent<Text>().text = bot.name;
           // copywin.transform.GetChild(1).GetComponent<Text>().text = bot.GetComponent<BotStats>().cost.ToString();
            //copywin.transform.GetChild(2).GetComponent<Text>().text = bot.GetComponent<BotStats>().Upgrades[0].ToString();
            
        }
    }

   /* public void BotSelect(GameObject text)
    {
       GameObject selected = GameObject.Find(text.GetComponentInChildren<Text>().text);

       //gameObject.GetComponentInParent<commandcenter>().bots.Add(selected);
       selected.transform.gameObject.GetComponentInChildren<Image>().color = Color.red;
    }
    */
    // Update is called once per frame

}
