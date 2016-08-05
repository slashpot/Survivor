using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class DisableCollider : RAINAction
{
	Collider collider;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		collider = ai.Body.GetComponent<SphereCollider> ();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		collider.enabled = false;
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}