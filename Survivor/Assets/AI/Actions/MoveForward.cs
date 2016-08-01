using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class MoveForward : RAINAction
{
	Rigidbody body;
	Transform transform;
	Vector3 movement;
	DetectWall detect;
	float speed = 150f;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		body = ai.Body.GetComponent<Rigidbody>();
		transform = ai.Body.GetComponent<Transform> ();
		detect = ai.Body.GetComponent<DetectWall> ();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		if (detect.enabled == false)
			detect.enabled = true;
		body.velocity = transform.forward * speed * 0.02f;
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}