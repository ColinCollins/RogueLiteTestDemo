using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseElf : MonoBehaviour
{
	public Transform Entity;

	// destination pos
	[HideInInspector]
	public GameObject Target;

	public bool isInit = false;
	public bool isSurvive = false;
	public bool isDead = false;

	protected Animator anim;

	#region Elf Prop

	[HideInInspector]
	public int Attack = 1;

	public float AttackRange = 10f;
	public float DetectRange = 20f;

	[HideInInspector]
	public float MoveSpeed = 10f;
	public float RotateSpeed = 20f;

	#endregion

	protected ElfState state = ElfState.Idle;

	public virtual void Init ()
	{
		
	}

}
