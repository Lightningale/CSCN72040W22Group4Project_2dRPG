using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
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
        Enemy hit=other.GetComponentInParent<Enemy>();
        int atk=GetComponentInParent<Player>().atk;
        if(hit!=null)
        {   
            //Debug.Log(gameObject.name+" Hit "+other.name);
            hit.TakeDamage(atk,transform.position);
        }
    }
}
