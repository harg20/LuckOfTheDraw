using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharmUI : MonoBehaviour
{
    public CharmScript charm;
    // Start is called before the first frame update
    void Start()
    {
        //var charmn = ScriptableObject.CreateInstance<CharmScript>();
        // charmn.CharmGenerator();
        //GetComponent<Text>().text = charmn.CharmTypes[0] + " " + charmn.CharmTypes[1] + " " + charmn.CharmTypes[2];
      
    }
    public void SetCharm(CharmScript charm)
    {
        GetComponent<Text>().text = charm.CharmTypes[0] + " " + charm.CharmTypes[1] + " " + charm.CharmTypes[2];
    }
    // Update is called once per frame
   
}
