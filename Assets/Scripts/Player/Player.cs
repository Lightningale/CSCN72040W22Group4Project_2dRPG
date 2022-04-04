using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
public class Player : MonoBehaviour
{
    private static Player instance;
    public GameObject charStatUI;
    public class SaveData
    {
        public int level{get;private set;}
        public int maxExp{get;private set;}
        public int exp{get;private set;}
        public int mana{get;private set;}
        public Vector3 position{get;private set;}
        public List<int>weaponList{get;private set;}
        //default constructor
        public SaveData(int level,int exp,Vector3 position,List<int>weapons)
        {
            this.level=level;
            this.exp=exp;
            this.position=position;
            this.weaponList=new List<int>(weapons);
        }
        public SaveData(SaveData copy)//deep copy constructor
        {
            this.level=copy.level;
            this.exp=copy.exp;
            this.position=copy.position;
            this.weaponList=new List<int>(copy.weaponList);
        }
    }
    public enum CharacterState
    {
        none,
        idle,
        run,
        onAir,
        die
    }
    public CharacterState previousState, currentState;
  //  [Header("Internal Stats")]

//**Memento should save data in this section and the gameobject's position***********
    public int level{get;private set;}
    public int exp{get;private set;}
    public int currentMana{get;private set;}
    public int currentHealth{get;private set;}
    public Vector3 position{get;private set;}
    public int maxExp{get;private set;}
    public int maxMana{get;private set;}
    public int maxHealth{get;private set;}
    public int atk{get;private set;}
    public List<int> weapons;
//***********************************************************************************
    [Header("Controls")]
    public Camera cam;
    [SerializeField]
    private float runSpeed=10f;
    [SerializeField]
    private float jumpSpeed=40;
    private float scale=1;
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
        Player.instance=this;
        previousState=CharacterState.none;
        currentState=CharacterState.idle;
        faceDirection=Vector2.right;
        rigidbody=GetComponent<Rigidbody2D>();
        collider=GetComponent<Collider2D>();
        animator=GetComponent<Animator>();
        groundLayer=LayerMask.GetMask("Ground");
        weaponList=spriteLibraryAsset.GetCategoryLabelNames(weaponCategory).ToArray();
        exp=0;
        UpdateStats();
        currentHealth=maxHealth;
        
        attackCollider=GetComponentInChildren<PlayerMeleeAttack>().transform.GetComponent<Collider2D>();
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
        RestoreMana();
    }
    public static Player GetInstance()
    {
       // if(Player.instance==null)
         //   Player.instance=
        return Player.instance;
    }
    public SaveData GenerateSave()
    {
        return new SaveData(level,exp,transform.position,weapons);
    }
    public void ResetState()
    {
        LoadSave(new SaveData(1,0,new Vector3(0,0,0),new List<int>(0)));
        //charStatUI.SetActive(true);
    }
    public void LoadSave(SaveData save)
    {
        level=save.level;
        exp=save.exp;
        transform.position=save.position;
        weapons=new List<int>(save.weaponList);
        UpdateStats();
    }
    public void AddExp(int amount)
    {
        exp+=amount;
        UpdateStats();
    }
    void UpdateStats()
    {
        maxExp=50+level*50;
        while(exp>maxExp)
        {
            exp-=maxExp;
            level++;
            maxExp=50+level*50;
        }
        atk=15+level*5;
        maxHealth=50+level*10;
        maxMana=80+level*20;
        currentMana=maxMana;
        currentHealth=maxHealth;
    }
   void RestoreMana()
   {
       if(currentMana<maxMana)
          currentMana++;
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
        if(currentHealth>0)
            currentHealth-=damage;
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
