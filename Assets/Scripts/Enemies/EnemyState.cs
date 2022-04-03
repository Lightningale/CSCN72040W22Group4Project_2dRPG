using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy subject;
    public EnemyState(Enemy subject)
    {
        this.subject=subject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Action()
    {

    }
}
public class EnemyIdle: EnemyState
{
    public Vector3 destination;
    public EnemyIdle(Enemy subject):base(subject)
    {
    if(subject.GetFaceDirection().x>0)
        destination=subject.pointRight;
    else
        destination=subject.pointLeft;
    }
    public override void Action()
    {
        base.Action();
        //Debug.Log(subject.name+" Idling");
        subject.animator.Play("Run",0);
        subject.SetHorizontalFlip(destination.x-subject.transform.position.x);
        subject.transform.position=Vector3.MoveTowards(subject.transform.position,destination,subject.GetSpeed()*Time.deltaTime);
        if(subject.transform.position.x<=subject.pointLeft.x)
        {
            destination=subject.pointRight;
        }
        else if(subject.transform.position.x>=subject.pointRight.x)
        {
            destination=subject.pointLeft;
        }
       // if(subject.FoundTarget())
        //{
         //   subject.ChangeState(new EnemyEngage(subject,target));
        //}
    }

}
public class EnemyEngage: EnemyState
{
    protected GameObject target;
   // private bool animationLock;
    
    public EnemyEngage(Enemy subject,GameObject target):base(subject)
    {
        this.target=target;
        //animationLock=false;
    }
    public override void Action()
    {
        base.Action();
        //Debug.Log(subject.name+" Engaging");
        if(!subject.attackLock)
            subject.animator.Play("Run",0);
        Vector3 destination=new Vector3(target.transform.position.x,subject.transform.position.y,subject.transform.position.z);
        subject.SetHorizontalFlip(destination.x-subject.transform.position.x);
        if(Vector3.Distance(destination,subject.transform.position)>=subject.attackRange)
            subject.transform.position=Vector3.MoveTowards(subject.transform.position,destination,subject.GetSpeed()*1.2f*Time.deltaTime);
        else
        {
            if(!subject.attackLock)
                subject.StartCoroutine("Attack");
        }
    }
}
