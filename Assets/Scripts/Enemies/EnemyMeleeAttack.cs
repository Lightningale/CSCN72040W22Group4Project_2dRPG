using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        //Debug.Log(gameObject.name+" Hit ");
        Player hit=other.GetComponentInParent<Player>();
        int atk=GetComponentInParent<Enemy>().atk;
        if(hit!=null)
        {   
            //Debug.Log(gameObject.name+" Hit "+other.name);
            hit.TakeDamage(atk,transform.position);
            
        }
    }
}
