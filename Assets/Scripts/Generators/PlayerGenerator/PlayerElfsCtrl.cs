using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElfsCtrl : MonoBehaviour
{
	private PlayerElfsPool soliderPool;

	// 增量
	public int SoliderElfAttack = 1;
	public float SoliderElfAttackRange = 2.5f;
	public float SoliderElfMoveSpeed = 15;

	public GameObject PlayerBaseHome;
	public List<SoliderElf> ExistList;

	// Start is called before the first frame update
	public void Init()
	{
		soliderPool = GetComponent<PlayerElfsPool>();
		soliderPool.Init();

		ExistList = new List<SoliderElf>();
	}

	public void GenerateNewSolider(Vector3 curPos)
	{
		var tempComp = soliderPool.PopObj();

		if (tempComp.isInit)
		{
			tempComp.gameObject.SetActive(true);
		}
		else
		{
			tempComp.transform.SetParent(soliderPool.parent.transform);
			tempComp.transform.localScale = Vector3.one;
			tempComp.transform.localEulerAngles = new Vector3(0, 0, 0);
			tempComp.Init(this);
		}

		tempComp.transform.localPosition = new Vector3(curPos.x, curPos.y, curPos.z + 8f);

		// 更新升级后的状态
		resetOriginState(tempComp);

		if (!tempComp.isInit)
			tempComp.Init(this);
		else
			tempComp.BornedStage();

		ExistList.Add(tempComp);
	}

	private void resetOriginState(SoliderElf elf) {
		elf.Attack = SoliderElfAttack;
		elf.AttackRange = SoliderElfAttackRange;
		elf.MoveSpeed = SoliderElfMoveSpeed;
	}

	private void Update()
	{
		if (GameManager.GetInstance().isPlaying())
			Recycle();
	}

	public void Recycle(bool isForce = false)
	{
		for (int i = 0; i < ExistList.Count; i++)
		{
			var solider = ExistList[i];
			if (solider.isDead || isForce)
			{
				soliderPool.PushObj(solider);
				ExistList.Remove(solider);
				i--;
			}
		}
	}

}
