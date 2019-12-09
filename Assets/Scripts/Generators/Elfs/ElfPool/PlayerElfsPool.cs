using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElfsPool : ElfsPool<SoliderElf>
{
	public override void Init()
	{
		base.Init();
	}

	public override SoliderElf PopObj()
	{
		return base.PopObj();
	}

	public override void PushObj(SoliderElf obj)
	{
		obj.gameObject.SetActive(false);

		base.PushObj(obj);
	}
}
