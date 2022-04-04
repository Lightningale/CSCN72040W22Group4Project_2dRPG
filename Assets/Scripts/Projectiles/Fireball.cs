using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Animator animator;
    Vector2 direction;
    float speed;
    int damage;
    bool flying;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        direction=new Vector2(1,0);
        animator.Play("Fly",0);
        //speed=10f;
        flying=true;
        Destroy(gameObject,5f);
        //Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        if(flying)
            transform.position=new Vector3(transform.position.x+speed*Time.deltaTime,transform.position.y,transform.position.z);
    }
    public void Initialize(Vector2 direction,int damage)
    {
        this.direction=direction;
        this.damage=damage;
        this.speed=direction.x*10f;
        if(direction.x>0)
            transform.rotation=Quaternion.Euler(0,0,0);
        else
            transform.rotation=Quaternion.Euler(0,180,0);
    

    }
     private void OnTriggerEnter2D(Collider2D other) {
        
        //Debug.Log(gameObject.name+" Hit ");
        
        Enemy hit=other.GetComponentInParent<Enemy>();
        //int atk=GetComponentInParent<Player>().atk;
        if(hit!=null)
        {   
            flying=false;
            animator.Play("Destroy",0);
            Destroy(gameObject,0.5f);
            //Debug.Log(gameObject.name+" Hit "+other.name);
            hit.TakeDamage(damage,transform.position);
        }
    }
}
