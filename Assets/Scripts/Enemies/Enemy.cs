using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public enum enemyState
    {
        None,
        Idle,
        Engage,
        Dead
    }
    [Header("Internal Stats")]
    public int level;
    public int atk;
    public int maxHealth;
    public int currentHealth;
    public int positionSlot;
    public float attackCooldown=0.5f;
    [Header("Movement")]
    public float speed;
    public float attackRange;
    public Vector3 pointLeft,pointRight,pointSpawn;

     [Header("Components")]
    protected new Rigidbody2D rigidbody;
    protected new Collider2D collider;
    protected Collider2D attackCollider;
    public Animator animator;

    [Header("Internal States")]
    protected EnemyState state;
    protected bool grounded;
    protected bool resetJump=false;
    protected bool attackReady=true;
    public bool attackLock=false;
    public bool hitLock=false;
   // protected float attackCooldown=0.16f;
    protected bool stateChanged;
    protected Vector2 faceDirection;
    [SerializeField]
    protected LayerMask groundLayer;
    // Start is called before the first frame update
    public virtual void Start()
    {
         faceDirection=Vector2.right;
        rigidbody=GetComponent<Rigidbody2D>();
        collider=GetComponent<Collider2D>();
        animator=GetComponent<Animator>();
        groundLayer=LayerMask.GetMask("Ground");
        pointSpawn=transform.position;
        pointLeft=pointSpawn-new Vector3(5,0,0);
        pointRight=pointSpawn+new Vector3(5,0,0);


        attackCollider=GetComponentInChildren<EnemyMeleeAttack>().transform.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(!hitLock&&!attackLock)
            state.Action();
    }
    public virtual bool IsGrounded()
    {
        RaycastHit2D hitGround=Physics2D.Raycast(transform.position,Vector2.down,0.1f,groundLayer.value);
        
        Debug.DrawRay(transform.position,Vector2.down*0.1f,Color.green);
        if(hitGround.collider!=null)
        {
            //if(!resetJump)
                return true;
        }
        return false;        
    }
    public void Initialize(int level,int positionID)
    {
        this.level=level;
        this.positionSlot=positionID;
    }
    public abstract void UpdateStats();
    public void ChangeState(EnemyState newState)
    {
        this.state=newState;
    }
    public Vector2 GetFaceDirection()
    {
        return faceDirection;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void FoundTarget(GameObject target)
    {
        if(state is EnemyIdle)
        {
            state = new EnemyEngage(this,target);
        }
        StartCoroutine("ResetVision");
    }
    public void TakeDamage(int damage, Vector3 source)
    {
        if(!hitLock)
        {
            SetHorizontalFlip(source.x-transform.position.x);
            currentHealth-=damage;
            if(currentHealth<=0)
            {
                currentHealth=0;
                 hitLock=true;
                animator.Play("Die",0);
                Destroy(gameObject,2f);
                EnemyManager.GetInstance().NotifyEnemyDeath(positionSlot);
            }
            else
            {
                animator.Play("Hit",0);
                StartCoroutine("HitCooldown");
            }
           
        }
        
    }
    public void SetHorizontalFlip(float direction)
    {
        if(direction>0)
        {
            transform.rotation=Quaternion.Euler(0,0,0);
            faceDirection=Vector2.right;
        }
        else if(direction<0)
        {
            transform.rotation=Quaternion.Euler(0,180,0);
            faceDirection=Vector2.left;
        }
    }
    protected virtual IEnumerator Attack()
    {
        attackLock=true;
        animator.Play("Attack",0);
        if(attackCollider!=null)
            attackCollider.enabled=true;
        yield return new WaitForSeconds(attackCooldown);
        attackLock=false;
        animator.Play("None",0);
        if(attackCollider!=null)
            attackCollider.enabled=false;
    }
    protected virtual IEnumerator Die()
    {
        hitLock=true;
        animator.Play("Die",0);
        yield return new WaitForSeconds(2f);
        Destroy(this,2f);
    }
    protected virtual IEnumerator ResetVision()
    {

        yield return new WaitForSeconds(5f);
        if(state is EnemyEngage)
        {
            state = new EnemyIdle(this);
        }
    }
    protected virtual IEnumerator HitCooldown()
    {
        hitLock=true;
        yield return new WaitForSeconds(0.5f);
        hitLock=false;
        animator.Play("None",0);
    }
}
