using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class ZombieAttack : RAINAction
{
	GameObject player;
	PlayerController controller;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		player = GameObject.FindGameObjectWithTag ("Player");
		controller = player.GetComponent<PlayerController> ();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		controller.Dead();
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}