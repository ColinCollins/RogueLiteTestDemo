using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElfsCtrl : MonoBehaviour
{
	private PlayerElfsPool soliderPool;

	// 增量
	public int SoliderElfAttack = 1;
	public float SoliderElfAttackRange = 15f;
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
			tempComp.BornedStage();
		}
		else
		{
			tempComp.transform.SetParent(soliderPool.parent.transform);
			tempComp.transform.eulerAngles = new Vector3(0, 0, 0);
			tempComp.Init(this);
		}

		float rx = Random.Range(-1f, 1f);
		float rz = Random.Range(1f, 2f);
		float rs = Random.Range(0.7f, 1.5f);
		// Debug.Log(rx + ": " + rz);

		tempComp.transform.localScale = Vector3.one * rs;
		tempComp.transform.localPosition = new Vector3(curPos.x + rx, curPos.y, curPos.z + rz);

		// 更新升级后的状态
		resetOriginState(tempComp);

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
