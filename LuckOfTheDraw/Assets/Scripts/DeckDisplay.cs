using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeckDisplay : MonoBehaviour
{
    public CardDataTracker cdt;
    public List<Sprite> cardSprites;
    public List<GameObject> spriteobj;
    public GameObject cardSpriteTemplate;
    // Start is called before the first frame update
    private void Start()
    {
        ResetDisplay();
        
       

    }
    public void ResetDisplay()
    {
        for(int i =0;i<spriteobj.Count;i++)
        {
            Destroy(spriteobj[i].gameObject);
        }
        spriteobj.Clear();
        cardSprites.Clear();
        UpdateDisplay();

    }
    public void UpdateDisplay()
    {
        for (int i = 0; i < cdt.currentCards.Count; i++)
        {
            

            if (!cardSprites.Contains(cdt.currentCards[i].image))
            {
                cardSprites.Add(cdt.currentCards[i].image);

                GameObject imog = Instantiate(cardSpriteTemplate);
                spriteobj.Add(imog);

                imog.transform.SetParent(transform);
                //imog.transform.localScale = transform.localScale * .5f;
                imog.GetComponent<Image>().sprite = cdt.currentCards[i].image;
                imog.GetComponent<AddCardToHand>().card = cdt.currentCards[i];
                //imog.transform.position = imog.transform.position = new Vector3(imog.transform.parent.position.x + ((i + 1) * 100), imog.transform.parent.position.y, 0);
            }
            

        }
        
    }
  
 
    // Update is called once per frame

}
