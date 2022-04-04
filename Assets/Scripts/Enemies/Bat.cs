using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public override void Start()
    {
        base.Start();
        ChangeState(new EnemyIdle(this));
        speed=2f;
        //level=1;
        attackRange=1.5f;
      //  UpdateStats();
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void UpdateStats()
    {
        atk=5+level*5;
        maxHealth=50+level*10;
        currentHealth=maxHealth;
    }
}
