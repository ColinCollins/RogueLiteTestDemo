using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseElf : MonoBehaviour
{
	// destination pos
	[HideInInspector]
	public GameObject Target;

	public bool isInit = false;
	public bool isSurvive = false;
	public bool isDead = false;

	protected Animator anim;
	protected AnimatorCover animCover;

	#region Elf Prop

	[HideInInspector]
	public int Attack = 1;
	[HideInInspector]
	public float AttackRange = 2.5f;
	public float DetectRange = 5;

	[HideInInspector]
	public float MoveSpeed = 10f;
	public float RotateSpeed = 20f;

	#endregion

	protected ElfState state = ElfState.Idle;

	public virtual void Init ()
	{
		
	}

}
