using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class SearchPlayer : RAINAction
{
	Vector3 player;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		player = GameObject.FindGameObjectWithTag ("Player").transform.position;
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		ai.WorkingMemory.SetItem<Vector3> ("varMoveTo", player);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}