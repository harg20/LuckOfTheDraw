using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeckDisplay : MonoBehaviour
{
    public CardDataTracker cdt;
    public List<Sprite> cardSprites;
    // Start is called before the first frame update
    private void Start()
    {
        
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

                var imog = Instantiate(new GameObject());

                imog.transform.parent = gameObject.transform;
                imog.transform.localScale = transform.localScale * .5f;
                var imig = imog.AddComponent<Image>();
                imig.sprite = cdt.currentCards[i].image;
                imog.transform.position = imog.transform.position = new Vector3(imog.transform.parent.position.x + ((i + 1) * 100), imog.transform.parent.position.y, 0);
            }



        }
    }
  
 
    // Update is called once per frame

}
