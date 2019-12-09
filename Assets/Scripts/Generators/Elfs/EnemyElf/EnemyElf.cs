using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EnemyAIAction(EnemyElf handle);

public class EnemyElf : BaseElf
{
	public bool isAttack = false;

	public ParticleSystem GeneratePar;
	public ParticleSystem ExplorePar;

	public AudioSource deadAudio;

	public EnemyAIAction action;
	public Animator personAnimator;
	protected EnemyActionsCallback actionCallback;

	// temporary
	public float life = 1;
	public float Life {
		get {
			return life;
		}

		set {
			life = value;
			if (life <= 0 && State != ElfState.Dead) {
				State = ElfState.Dead;
				deadAudio.Play();
			}
		}
	}

	public ElfState State
	{
		set
		{
			state = value;
			switch (state)
			{
				case ElfState.Idle:
					action = EnemyActions.Idle;
					break;
				case ElfState.GoStright:
					action = EnemyActions.MoveStraight;
					break;
				case ElfState.GoDestination:
					action = EnemyActions.MoveDestination;
					break;
				case ElfState.FindEnemy:
					action = EnemyActions.FindEnemy;
					break;
				case ElfState.Attack:
					action = EnemyActions.Attack;
					action(this);
					break;
				case ElfState.Dead:
					// Dead 不需要循环执行
					action = EnemyActions.Dead;
					action(this);
					break;
				default:
					break;
			}
		}
		get
		{
			return state;
		}
	}
	private EnemyElfsCtrl manager;

	public void Init (EnemyElfsCtrl manager)
	{
		isInit = true;
		isDead = false;
		Target = null;
		this.manager = manager;

		Entity = transform.Find("Entity");
		anim = GetComponentInChildren<Animator>();
		actionCallback = GetComponentInChildren<EnemyActionsCallback>();
		actionCallback.Init(this);

		BornedStage();
	}

	public void ResetProp()
	{
		Target = null;
		isDead = false;
		isSurvive = false;
		Life = 1;
	}

	public void BornedStage()
	{
		State = ElfState.Idle;
		GeneratePar.Play();

		personAnimator.SetBool("Dash", true);
		ResetProp();
	}

	private void Update()
	{
		if (State == ElfState.Dead || State == ElfState.Attack || State == ElfState.FindEnemy) return;

		if (action != null) action(this);
	}

	#region Borned(Generate) Animation

	public void PlayBornedAnim()
	{
		anim.SetTrigger("Generate");
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
		anim.SetTrigger("Attack");
	}

	public void AttackFinished()
	{
		Debug.Log("AttackFinished");
		// Target get damage

		if (isSurvive)
		{
			// CenterCtrl.GetInstance().PCtrl.Life--;
		}

		State = ElfState.Dead;
	}

	#endregion

	#region Dead State

	// 播放死亡
	public void PlayDead()
	{
		anim.SetTrigger("Dead");
	}

	// DeadAnimFinished
	public void DeadFinished()
	{
		Debug.Log("Enemy Dead");
		ExplorePar.Play();
		isDead = true;

		if (isSurvive) {
			CenterCtrl.GetInstance().PCtrl.Life--;
		}
	}

	#endregion

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("Boom")) {

			float attack = other.transform.parent.tag.Equals("Weapon") ?
				other.transform.parent.GetComponent<BulletElf>().Attack:
			other.transform.parent.GetComponent<SoliderElf>().Attack;

			Life -= attack;
		}
	}
}
