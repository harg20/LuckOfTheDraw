using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Charm", menuName = "Charm")]
public class CharmScript : ScriptableObject
{
    public enum Effect {oddsBoost,bulletMod, slotMachine};
    public Effect CharmType;
    // Start is called before the first frame update
    
}
