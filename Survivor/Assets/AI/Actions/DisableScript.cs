using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class DisableScript : RAINAction
{
	DetectWall detect;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		detect = ai.Body.GetComponent<DetectWall> ();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		detect.enabled = false;
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}