using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElf : BaseElf
{

	#region Properties

	private Move moveComp;
	private Life lifeComp;
	private Attack atkComp;
	private AnimCtrl animComp;

	#endregion 

	private PlayerElf() { }

	public static PlayerElf Create(GameObject body) {
		return null;
	}

	public static PlayerElf Clone(PlayerElf elf) {
		return null;
	}

	public override void Init()
	{
		base.Init();
		

	}
}
