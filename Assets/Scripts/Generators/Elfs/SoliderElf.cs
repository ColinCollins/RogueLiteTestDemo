﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public delegate void SoliderAIAction(SoliderElf handle);

public class SoliderElf : BaseElf
{
	private PlayerElfsCtrl manager;
	private SoliderSelector selector;

	public SoliderAIAction action;
	public Transform boomTrigger;

	public ElfState State {
		set {
			state = value;
			switch (state) {
				case ElfState.Idle:
					action = SoliderActions.Idle;
					break;
				case ElfState.GoStright:
					action = SoliderActions.MoveStraight;
					break;
				case ElfState.GoDestination:
					action = SoliderActions.MoveDestination;
					break;
				case ElfState.FindEnemy:
					action = SoliderActions.FindEnemy;
					action(this);
					break;
				case ElfState.Attack:
					action = SoliderActions.Attack;
					action(this);
					break;
				case ElfState.Dead:
					// Dead 不需要循环执行
					action = SoliderActions.Dead;
					action(this);
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

		animCover = GetComponent<AnimatorCover>();
		anim = GetComponentInChildren<Animator>();
		animCover.Init(anim);

		selector = GetComponent<SoliderSelector>();
		selector.Init(this);

		BornedStage();
	}

	public void ResetProp()
	{
		Target = null;
		isDead = false;
		isSurvive = false;
	}

	public void BornedStage()
	{
		ResetProp();
		State = ElfState.Idle;
	}

	private void Update()
	{
		if (State == ElfState.Dead || State == ElfState.Attack || State == ElfState.FindEnemy) return;

		action(this);
	}

	#region Borned(Generate) Animation

	public void PlayBornedAnim() {
		animCover.PlaySpecialAnim("Generate", BornedFinished);
	}

	// Animation Callback
	public void BornedFinished()
	{
		anim.SetBool("Moving", true);
		State = ElfState.GoStright;
	}

	#endregion

	#region Attack State

	public void PlayAttack()
	{
		anim.SetBool("Moving", false);
		animCover.PlaySpecialAnim("Attack", AttackFinished);
	}

	public void AttackFinished()
	{
		Debug.Log("AttackFinished");
		// Target get damage

		if (isSurvive) {
			CenterCtrl.GetInstance().ECtrl.Life--;
		}

		// create a trigger collider
		Sequence action = DOTween.Sequence();
		action.Append(boomTrigger.transform.DOScale(Vector3.one * AttackRange, 0.2f));

		action.AppendCallback(() => {
			boomTrigger.transform.DOScale(Vector3.zero, 0f);
			State = ElfState.Dead;
		});
	}

	#endregion

	#region FindEnemy State

	public void PlayScared() {
		anim.SetBool("Moving", false);
		animCover.PlaySpecialAnim("Scared", FindEnemyAnimFinished);
	}

	public void FindEnemyAnimFinished() {
		// move to
		State = ElfState.GoDestination;

		anim.SetBool("Moving", true);

		Debug.Log("FindEnemyFinished");
	}

	#endregion

	#region Dead State

	// 播放死亡
	public void PlayDead() {
		animCover.PlaySpecialAnim("Dead", DeadFinished);
	}

	// DeadAnimFinished
	public void DeadFinished() {
		Debug.Log("Solider Dead");
		isDead = true;
	}

	#endregion
}
