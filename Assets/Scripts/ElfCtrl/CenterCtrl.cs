using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 用于控制角色对象碰撞攻击检测， 
public class CenterCtrl : MonoBehaviour
{
	[HideInInspector]
	public PlayerCtrl PCtrl;
	[HideInInspector]
	public EnemyCtrl ECtrl;

	private PlayerElfsCtrl playerElfsCtrl = null;
	private EnemyElfsCtrl enemyElfsCtrl = null;

	private GameManager manager = null;

	private static CenterCtrl instance;
	public static CenterCtrl GetInstance() {
		return instance;
	}

	public void Init(GameManager manager)
	{
		instance = this;

		PCtrl = GetComponent<PlayerCtrl>();
		PCtrl.Init(this);

		ECtrl = GetComponent<EnemyCtrl>();
		ECtrl.Init(this);

		playerElfsCtrl = PCtrl.PEPool;
		enemyElfsCtrl = ECtrl.EEPool;
	}


	public void ResetScene()
	{
		PCtrl.ResetScene();
		ECtrl.ResetScene();
	}


	void Update()
	{
		if (GameManager.GetInstance().isPlaying()) {
			PlayerSoliderDetected();
			EnemyDetected();
		}
	}

	private void PlayerSoliderDetected()
	{
		for (int i = 0; i < playerElfsCtrl.ExistList.Count; i++)
		{
			var solider = playerElfsCtrl.ExistList[i];
			var localPos = solider.transform.localPosition;
			float dis = 0;

			for (int j = 0; j < enemyElfsCtrl.ExistList.Count; j++)
			{
				var enemy = enemyElfsCtrl.ExistList[j];
				Vector3 pos = enemy.transform.localPosition;
				dis = Vector3.Distance(localPos, pos);

				if (dis < (solider.DetectRange * 5) && solider.State == ElfState.GoStright)
				{
					solider.Target = enemy.gameObject;
					solider.State = ElfState.FindEnemy;
				}
			}

			float targetZ = enemyElfsCtrl.EnemyBaseHome.transform.localPosition.z;
			float dt = targetZ - localPos.z;

			if (dt < solider.AttackRange && !solider.isSurvive) {
				solider.State = ElfState.Attack;
				solider.isSurvive = true;
			}
		}
	}

	private void EnemyDetected()
	{
		for (int i = 0; i < enemyElfsCtrl.ExistList.Count; i++)
		{
			var enemy = enemyElfsCtrl.ExistList[i];
			var localPos = enemy.transform.localPosition;

			float targetZ = playerElfsCtrl.PlayerBaseHome.transform.localPosition.z;
			float dt = localPos.z - targetZ;

			if (dt < 10 && !enemy.isSurvive)
			{
				enemy.State = ElfState.Attack;
				enemy.isSurvive = true;
			}

			if (!enemy.isAttack) return;
		}
	}
}
