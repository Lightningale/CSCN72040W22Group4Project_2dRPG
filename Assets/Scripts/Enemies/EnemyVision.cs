using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other) {
        Enemy thisEnemy=GetComponentInParent<Enemy>();
       
        Player target=other.GetComponent<Player>();
        if(target!=null)
        {   
             Debug.Log(this.name+" Vision triggered");
            //if(target.IsAlive())
            thisEnemy.FoundTarget(target.gameObject);
        }
    }
}
