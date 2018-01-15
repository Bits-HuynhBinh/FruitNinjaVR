using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStateMachine : StateMachine
{
    private GameObject boss;
    private BossController bossController;

    public MyStateMachine(GameObject boss)
    {
        this.boss = boss;
        bossController = this.boss.GetComponent<BossController>();
    }

    public override void OnStateANIM()
    {
        bossController.MakeColor();
    }

    public override void OnStateIDLE()
    {
        bossController.Idle();
    }

    public override void OnStateMOVE()
    {
        bossController.Move();
    }

    public override void OnStateSHOOT()
    {
        bossController.Shoot();
    }
}
