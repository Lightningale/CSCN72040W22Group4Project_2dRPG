using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
public class Player : MonoBehaviour
{
    public enum CharacterState
    {
        none,
        idle,
        run,
        onAir,
        die
    }
    public CharacterState previousState, currentState;
    [Header("Internal Stats")]
    public int exp;
    public int level;
    public int atk;
    public int maxHealth;
    public int currentHealth;
    public List<int> inventory;
    [Header("Controls")]
    public Camera cam;
    public float runSpeed=10f;
    public float jumpSpeed=40;
    public float scale=1;
    [SerializeField]
    protected LayerMask groundLayer;
    
    
    [Header("Inputs")]
    protected float horizontalInput,verticalInput;
    protected bool clickJump,releaseJump,clickPrimary;
    [Header("Components")]
    protected new Rigidbody2D rigidbody;
    protected new Collider2D collider;
    protected Collider2D attackCollider;
    protected Animator animator;
    [SerializeField]
    protected SpriteLibrary spriteLibrary=default;
    protected SpriteLibraryAsset spriteLibraryAsset=>spriteLibrary.spriteLibraryAsset;
    [SerializeField]
    private SpriteResolver spriteResolver=default;
    [SerializeField]
    protected string weaponCategory=default;
    
    protected string[] weaponList;
    [Header("Internal States")]
    protected bool grounded;
    protected bool resetJump=false;
    protected bool attackReady=true;
    protected float attackCooldown=0.16f;
    protected bool stateChanged;
    protected bool stateLock=false;
    protected Vector2 faceDirection;
    // Start is called before the first frame update
    void Start()
    {
        previousState=CharacterState.none;
        currentState=CharacterState.idle;
        faceDirection=Vector2.right;
        rigidbody=GetComponent<Rigidbody2D>();
        collider=GetComponent<Collider2D>();
        animator=GetComponent<Animator>();
        groundLayer=LayerMask.GetMask("Ground");
        weaponList=spriteLibraryAsset.GetCategoryLabelNames(weaponCategory).ToArray();
        UpdateExp();
        currentHealth=maxHealth;

        attackCollider=GetComponentInChildren<PlayerMeleeAttack>().transform.GetComponent<Collider2D>();
        /*foreach(Transform tr in transform)
        {
            if(tr.tag=="AttackCollider")
            {
                Debug.Log("Player found attack collider");
                attackCollider=tr.GetComponent<Collider2D>();
                break;
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {   
        grounded=IsGrounded();
        stateChanged=previousState!=currentState;
        previousState=currentState;
        GetPlayerInput();
        SetHorizontalFlip(horizontalInput);
        if(!stateLock)
            StateMachine();
        SwitchWeapon();
    }
    void UpdateExp()
    {
        level=exp/100;
        atk=5+level*5;
        maxHealth=50+level*10;

    }
   
    void StateMachine()
    {
        switch(currentState)
        {
            case CharacterState.idle:
            Idle();
            break;
            case CharacterState.run:
            Run();
            break;
            case CharacterState.onAir:
            OnAir();
            break;
            default:
            break;
        }
    }
    void SwitchWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            spriteResolver.SetCategoryAndLabel(weaponCategory,weaponList[0]);
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            spriteResolver.SetCategoryAndLabel(weaponCategory,weaponList[1]);   
        else if(Input.GetKeyDown(KeyCode.Alpha3))
            spriteResolver.SetCategoryAndLabel(weaponCategory,weaponList[2]); 
        else if(Input.GetKeyDown(KeyCode.Alpha4))
            spriteResolver.SetCategoryAndLabel(weaponCategory,weaponList[3]); 
        else if(Input.GetKeyDown(KeyCode.Alpha5))
            spriteResolver.SetCategoryAndLabel(weaponCategory,weaponList[4]); 

    }
    protected void Idle()
    {
        animator.Play("Idle",0);
        //animator.
        if(clickPrimary&&attackReady)
        {
            
            StartCoroutine("Attack");
        }
            
        if(!grounded)
            currentState=CharacterState.onAir;
        else if(clickJump&&attackReady)
        {
            rigidbody.velocity=new Vector2(rigidbody.velocity.x,jumpSpeed);
            currentState=CharacterState.onAir;
        }       
        else if(horizontalInput!=0)
        {
            currentState=CharacterState.run;
        }
        
    }
    protected void Run()
    {
        animator.Play("Run",0);
        rigidbody.velocity=new Vector2(horizontalInput*runSpeed,rigidbody.velocity.y);
        if(clickPrimary&&attackReady)
        {
            StartCoroutine("Attack");
        }
            
        if(!grounded)
            currentState=CharacterState.onAir;
        else if(clickJump)
        {
            rigidbody.velocity=new Vector2(rigidbody.velocity.x,jumpSpeed);
            currentState=CharacterState.onAir;
        }    
        else if(horizontalInput==0)
            currentState=CharacterState.idle;
    }
    protected void OnAir()
    {
        animator.Play("OnAir",0);
        if(clickPrimary&&attackReady)
        {
            StartCoroutine("Attack");
        }   
        rigidbody.velocity=new Vector2(horizontalInput*runSpeed*0.75f,rigidbody.velocity.y);
        if(grounded)
            currentState=CharacterState.idle;
    }
    protected void attack()
    {

    }
    protected void die()
    {

    }
     public void TakeDamage(int damage,Vector3 source)
    {

        maxHealth-=damage;
        animator.Play("Hit",0);
        SetHorizontalFlip(source.x-transform.position.x);
        StartCoroutine("HitCooldown");
    }
    protected void SetHorizontalFlip(float direction)
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
    protected void GetPlayerInput()
    {

        horizontalInput=Input.GetAxisRaw("Horizontal");//Only -1 or 1
        verticalInput=Input.GetAxisRaw("Vertical");
        clickJump=Input.GetButtonDown("Jump");
        releaseJump=Input.GetButtonUp("Jump");
        clickPrimary=Input.GetMouseButtonDown(0);
        
    }
    public virtual bool IsGrounded()
    {
        RaycastHit2D hitGround=Physics2D.Raycast(transform.position,Vector2.down,0.1f*scale,groundLayer.value);
        
        Debug.DrawRay(transform.position,Vector2.down*0.1f*scale,Color.green);
        if(hitGround.collider!=null)
        {
            //if(!resetJump)
                return true;
        }
        return false;        
    }
     protected virtual IEnumerator Attack()
    {
        animator.Play("Attack",1);
        if(attackCollider!=null)
            attackCollider.enabled=true;
        attackReady=false;
        yield return new WaitForSeconds(attackCooldown);
        attackReady=true;
        animator.Play("None",1);
        if(attackCollider!=null)
            attackCollider.enabled=false;
    }
    protected virtual IEnumerator HitCooldown()
    {
        stateLock=true;
        yield return new WaitForSeconds(attackCooldown);
        stateLock=false;
        animator.Play("None",0);
    }
}
