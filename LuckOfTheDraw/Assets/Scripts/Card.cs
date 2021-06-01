using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    // Start is called before the first frame update
    public new string name;
    
    public Sprite image;
    public int splitstackcount;
    public bool hitscan;
    public int rarity;
    public int projectiles;
    public int baseProjectiles;
    public int damage;
    public int baseDamage;
    public enum cardType { Bullet, Blank, Booster};
    public cardType CardType;
    public enum effectType {none, split, changeProjectile, smokeScreen, homing, shield, teleport, addSplitToNext, addBuffToNext, lifesteal, velocityBoost, explosiveBarrel};
    public effectType EffectType;
    //public List<effectType> inheritedEffects;
    public string effects;

    public void ResetInherited()
    {
        projectiles = baseProjectiles;
        damage = baseDamage;
    }


    public void updateDescription(int currentProj)
    {
        //Debug.Log(projectiles + ", " + stackcount);

        //projectiles = baseProjectiles + stackcount;
        if (CardType == cardType.Bullet)
        {
            
            effects = "Adds " + EffectType +  "  fires " + currentProj + " Dealing " + damage + " Damage each";  
         
        }
        if (CardType == cardType.Blank)
        {
            effects = "Adds " + EffectType;
        }
        if (CardType == cardType.Booster)
        {
            effects = "Adds " + EffectType + " to next bullet or blank, will stack";
        }
        
    }
}
