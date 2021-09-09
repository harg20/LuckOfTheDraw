using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
   

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "EnemyBullet")
        {

            boom();
        }
       

    }
   
    public void boom()
    {
        var boom = Instantiate(explosion, transform.position, Quaternion.identity);
        boom.transform.localScale = boom.transform.localScale * 3;
      
            Destroy(gameObject);
        
       
    }
    // Update is called once per frame

}
