using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RandomTurn : RAINAction
{
	Rigidbody body;
	Quaternion turn;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		body = ai.Body.GetComponent<Rigidbody>();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		turn = Quaternion.Euler (0f, Random.Range (0f, 360f), 0f);
		body.MoveRotation (turn);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}