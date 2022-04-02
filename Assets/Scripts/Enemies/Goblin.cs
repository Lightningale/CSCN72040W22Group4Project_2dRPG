using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ChangeState(new EnemyIdle(this));
        speed=2f;
        attackRange=1f;
        atk=50;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
