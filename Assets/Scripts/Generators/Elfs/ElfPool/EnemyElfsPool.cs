using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElfsPool : ElfsPool<EnemyElf>
{
	public override void Init()
	{
		base.Init();
	}

	public override EnemyElf PopObj()
	{
		return base.PopObj();
	}

	public override void PushObj(EnemyElf obj)
	{
		obj.gameObject.SetActive(false);

		base.PushObj(obj);
	}
}
