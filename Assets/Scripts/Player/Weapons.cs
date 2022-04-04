using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected Player holder;
    protected int atk;
    public Weapon(Player subject)
    {
        holder=subject;
        atk=holder.atk;
    }
    public abstract void Attack();
}
public class Sword:Weapon
{
    public Sword(Player subject):base(subject)
    {

    }
    public override void Attack()
    {

    }
  
}
public class Wand:Weapon
{
    private GameObject projectile;
    public Wand(Player subject):base(subject)
    {
        projectile=Resources.Load<GameObject>("Projectiles/Fireball");
    }
    public override void Attack()
    {
        if(holder.currentMana>holder.atk)
        {
            GameObject shot=GameObject.Instantiate(projectile,new Vector3(holder.transform.position.x+0.5f*holder.faceDirection.x,holder.transform.position.y+0.5f,holder.transform.position.z),new Quaternion());
            shot.GetComponent<Fireball>().Initialize(holder.faceDirection,holder.atk);
            holder.ConsumeMana(holder.atk);
        }
            
    }

}