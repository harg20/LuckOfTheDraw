using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shoot : MonoBehaviour
{
    RaycastHit hit;
    public Card currentCard;
    public Material shotmat;
    public GenerateHand handholder;
    public GameObject bulletProj;
    public GameObject smokeEffect;
    public GameObject hitEffect;
    public GameObject explosionEffect;
    public float spreadCone;
    float goproj;
    GameObject nextbullet;
    public float boostforce = 0;

    public Transform muzzle;
    bool first = false;
    Camera maincam;
    // Start is called before the first frame update
    void Start()
    {
        maincam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Space))
        {
            shoot();
            for (int i = 0; i < handholder.hand.Count; i++)
            {
                if (handholder.hand[i].GetComponent<CardDisplay>().splitstackcount > 0)
                handholder.hand[i].GetComponent<CardDisplay>().UpdateCardDisplay();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && handholder.hand.Count < handholder.handSize)
        {
            handholder.NewHand();
            Debug.Log("reload");
        }
        
            //Debug.Log(hit);
      

    }

    void shoot()
    {
        List<GameObject> hand = handholder.hand;
        
        if (hand[0]) currentCard = hand[0].GetComponent<CardDisplay>().card;
       
        if ( hand.Count >= 1 && !EventSystem.current.IsPointerOverGameObject())
        {
            switch (currentCard.EffectType)
            {
                case Card.effectType.split:

                    
                    CreateSpread(spreadCone);
                    

                    handholder.RemoveCard();
                    break;

                case Card.effectType.smokeScreen:
                    //Debug.Log("nope");
                    Instantiate(smokeEffect, muzzle.position, Quaternion.Euler(-90,0,0));
                    handholder.RemoveCard();
                    break;

                case Card.effectType.none:
                    //Debug.Log("basic");
                    
                    CreateSpread(spreadCone/2f);
                    

                    handholder.RemoveCard();
                    break;

                case Card.effectType.velocityBoost:
                    var boom = Instantiate(explosionEffect);
                    boom.transform.position = new Vector3(muzzle.position.x, muzzle.position.y, muzzle.position.z);
                    GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * boostforce);

                    handholder.RemoveCard();
                    break;

                case Card.effectType.addSplitToNext:
                    
                    //GameObject nextproj;
                    for (int i = 0; i < hand.Count; i++)
                    {
                        if ( hand[i].GetComponent<CardDisplay>().card.CardType == Card.cardType.Bullet)
                        {
                            nextbullet = hand[i];
                            
                            break;
                            
                        }
                    }
                    if (nextbullet && !nextbullet.GetComponent<CardDisplay>().effectslist.Contains(Card.effectType.addSplitToNext))
                    {
                        nextbullet.GetComponent<CardDisplay>().effectslist.Add(Card.effectType.addSplitToNext);
                        nextbullet.GetComponent<CardDisplay>().splitstackcount++;
                    }
                    else if (nextbullet && nextbullet.GetComponent<CardDisplay>().effectslist.Contains(Card.effectType.addSplitToNext))
                    {
                        nextbullet.GetComponent<CardDisplay>().splitstackcount++;
                    }
                   
                    if (nextbullet)
                    {
                        nextbullet.GetComponent<CardDisplay>().UpdateCardDisplay();

                    }
                    

                    handholder.RemoveCard();

                    break;

                case Card.effectType.lifesteal:
                    for (int i = 0; i < hand.Count; i++)
                    {
                        if (hand[i].GetComponent<CardDisplay>().card.CardType == Card.cardType.Bullet)
                        {
                            nextbullet = hand[i];

                            break;

                        }
                    }

                    if (nextbullet && !nextbullet.GetComponent<CardDisplay>().effectslist.Contains(Card.effectType.lifesteal))
                    {
                        nextbullet.GetComponent<CardDisplay>().effectslist.Add(Card.effectType.lifesteal);
                        nextbullet.GetComponent<CardDisplay>().lifestealstackcount++;
                    }
                    else if (nextbullet && nextbullet.GetComponent<CardDisplay>().effectslist.Contains(Card.effectType.lifesteal))
                    {
                        nextbullet.GetComponent<CardDisplay>().lifestealstackcount++;
                    }

                    if (nextbullet)
                    {
                        nextbullet.GetComponent<CardDisplay>().UpdateCardDisplay();

                    }

                    handholder.RemoveCard();
                    break;
            }

        }
        if (Input.GetKey(KeyCode.Mouse0) && hand.Count < 1)
        {
            handholder.NewHand();
        }
     
        

        void CreateSpread(float spread)
        {

            var pellets = hand[0].GetComponent<CardDisplay>().splitstackcount + currentCard.baseProjectiles; // whatever, I'm not a gun expert
            var points = new Vector2[pellets];
            var gd = new GaussianDistribution(); // maybe send a Random state through the ctor? I don't really use Unity's random any more
            for (int i = 0; i < pellets; i++)
            {
                points[i] = new Vector2(gd.Next(0f, spread, -spread, spread), gd.Next(0f, spread, -spread, spread)); // y component is practically for free, check the code
            }

            var rays = new Ray[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                
                // do a transformation/move points forward to fix them a certain distance from your ray origin,
                // like in target practice, so that you have control over the actual spread size; say 6 meters
                var p3d = new Vector3( transform.forward.x + points[i].x , transform.forward.y , transform.forward.z + points[i].y); // oh we need to make them 3D anyway
                rays[i] = new Ray(muzzle.position,  p3d.normalized); // use a ray origin of zero, make everything gun-centric
                
                if (Physics.Raycast(rays[i],out hit, 500,9) )
                {

                    var proj = Instantiate(bulletProj,muzzle.position,Quaternion.identity);
                    proj.transform.LookAt(hit.point);
                    /*
                    var shot = new GameObject();
                    shot.AddComponent<hit>();
                    var shotrenderer = shot.AddComponent<LineRenderer>();
                    shotrenderer.startWidth = .1f;
                    shotrenderer.positionCount = 2;
                    shotrenderer.SetPosition(0, muzzle.position);
                    shotrenderer.SetPosition(1, hit.point);
                    shotrenderer.material = shotmat;
                    */

                    if (hit.collider.tag == "Enemy")
                    {
                        hit.transform.GetComponent<Enemy>().TakeDamage(currentCard.damage);
                        Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        if (hand[0].GetComponent<CardDisplay>().effectslist.Contains(Card.effectType.lifesteal))
                        {
                            Debug.Log("stealhp");
                            GetComponent<PlayerHealth>().AddHealth((currentCard.damage / 2) + (hand[0].GetComponent<CardDisplay>().lifestealstackcount / 2));
                        }
                    }


                   
                }
              
            
            }
        }

    }
}
