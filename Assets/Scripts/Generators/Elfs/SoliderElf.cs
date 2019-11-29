using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderElf : MonoBehaviour
{
	private Animator anim;
	private PlayerElfsCtrl manager;
	private SoliderSelector selector;

	public delegate void AIAction(SoliderElf handle);
	public AIAction action;

	#region 

	public int Attack = 1;
	public float AttackRange = 2.5f;

	public float MoveSpeed = 10f;
	public float RotateSpeed = 20f;

	#endregion

	public bool isInit = false;
	public bool isDead = false;

	// destination pos
	[HideInInspector]
	public GameObject Target;

	private SoliderState state = SoliderState.Idle;
	public SoliderState State {
		set {
			state = value;
			switch (state) {
				case SoliderState.Idle:
					action = SoliderActions.Idle;
					break;
				case SoliderState.GoStright:
					action = SoliderActions.MoveStraight;
					break;
				case SoliderState.GoDestination:
					action = SoliderActions.MoveDestination;
					break;
				case SoliderState.FindEnemy:
					action = SoliderActions.FindEnemy;
					break;
				case SoliderState.Attack:
					action = SoliderActions.Attack;
					break;
				case SoliderState.Dead:
					action = SoliderActions.Dead;
					break;
				default:
					break;
			}
		}
		get {
			return state;
		}
	}

	public void Init(PlayerElfsCtrl manager)
	{
		isInit = true;
		isDead = false;
		Target = null;
		this.manager = manager;
		anim = GetComponentInChildren<Animator>();

		selector = GetComponent<SoliderSelector>();
		selector.Init(this);

		BornedStage();
	}

	public void BornedStage()
	{
		State = SoliderState.Idle;

		Target = null;
		isDead = false;

		StartCoroutine(BornedFinished());
	}

	private void Update()
	{
		action(this);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("DashLine")) {
			State = SoliderState.GoDestination;
			Target = manager.EnemyDestination;
		}
	}

	// Animation Callback
	public IEnumerator BornedFinished() {
		anim.SetBool("Generate", true);

		while (
			anim.GetCurrentAnimatorStateInfo(0).IsName("Generate") &&
			anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f
		) {
			yield return null;
		}

		anim.SetBool("Generate", false);
		anim.SetBool("Moving", true);

		State = SoliderState.GoStright;
	}

	public void DeadAnimFinished() {
		anim.SetBool("Dead", false);
		isDead = true;
	}

}
