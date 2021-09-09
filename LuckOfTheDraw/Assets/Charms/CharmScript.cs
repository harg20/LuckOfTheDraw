using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Charm", menuName = "Charm")]
public class CharmScript : ScriptableObject
{
    public enum Effect {none,oddsBoost,BulletChain, slotMachine};
    public Effect[] CharmTypes;
    bool first = true;
    public CardDataTracker cdt;
    public Mesh charmMesh;
    
    
    //make this an array of effects rather than having 3 separate variables, you donkey

    // Start is called before the first frame update
    public void CharmGenerator()
    {
        CharmTypes = new Effect[3];
        

        for (int i = 0; i < CharmTypes.Length; i++)
        {
            if (first == false)
            {
                int randi = Random.Range(0, CharmTypes.Length);

                CharmTypes[i] = (Effect)randi;
                if (CharmTypes[i] == Effect.none && i+1 < CharmTypes.Length)
                {
                    CharmTypes[i + 1] = Effect.none;
                    break;
                }
            }
            if (first == true)
            {
                int randi = Random.Range(1, CharmTypes.Length);
                
                CharmTypes[i] = (Effect)randi;
                first = false;
            }
           

        }
        AdjustCharmStats();
    }

    void AdjustCharmStats()
    {
        var dict = new Dictionary<int, int>();

        foreach (Effect value in CharmTypes)
        {
            if (dict.ContainsKey((int)value))
                dict[(int)value]++;
            else
                dict[(int)value] = 1;
        }
        foreach (var pair in dict)
        {
            Debug.Log(pair.Key + ", " + pair.Value);
            //add bonuses of matching effects together
        }
            
       

    }
}
