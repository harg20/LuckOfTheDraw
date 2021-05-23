using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text Name;
    public Text Description;
    public Image art;
    public Text effects;
    public Text splitstacks;
    public Text lifestealstacks;
     public int splitstackcount = 0;
    public int lifestealstackcount = 0;
    public List<Card.effectType> effectslist;
    public bool isboosted = false;
    // Start is called before the first frame update
    private void Start()
    {
        UpdateCardDisplay();
    }

   
    public void UpdateCardDisplay()
    {
        Name.text = card.name;
        Description.text = card.effects;
        art.sprite = card.image;

        lifestealstacks.text = lifestealstackcount.ToString();

        
        splitstacks.text = splitstackcount.ToString();
        card.updateDescription(card.baseProjectiles + splitstackcount);
    }

    // Update is called once per frame

}
