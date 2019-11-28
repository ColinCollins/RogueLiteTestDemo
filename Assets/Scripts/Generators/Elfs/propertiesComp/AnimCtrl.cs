using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : PropertiesComp
{
	private Animator ctrl;
	public Animator Ctrl {
		get {
			return ctrl;
		}
	}

	public override void Init()
	{
		ctrl = GetComponentInChildren<Animator>();
	}


}
