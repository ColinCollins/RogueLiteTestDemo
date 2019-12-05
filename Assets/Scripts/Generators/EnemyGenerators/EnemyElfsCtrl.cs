using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElfsCtrl : MonoBehaviour
{
	private EnemyElfsPool enemyElfsPool;

	public GameObject EnemyBaseHome;
	public List<EnemyElf> ExistList;

	// Start is called before the first frame update
	public void Init()
	{
		enemyElfsPool = GetComponent<EnemyElfsPool>();
		enemyElfsPool.Init();

		ExistList = new List<EnemyElf>();
	}

	public void GenerateNewSolider(Vector3 curPos)
	{
		var tempComp = enemyElfsPool.PopObj();

		if (tempComp.isInit)
		{
			tempComp.gameObject.SetActive(true);
			tempComp.BornedStage();
		}
		else {
			tempComp.transform.SetParent(enemyElfsPool.parent.transform);
			tempComp.transform.localScale = Vector3.one;
			tempComp.transform.localEulerAngles = new Vector3(0, 0, 0);
			tempComp.Init(this);
		}

		tempComp.transform.localPosition = new Vector3(curPos.x, curPos.y, curPos.z);

		ExistList.Add(tempComp);
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
			var enemy = ExistList[i];
			if (enemy.isDead || isForce)
			{
				enemyElfsPool.PushObj(enemy);
				ExistList.Remove(enemy);
				i--;
			}
		}
	}
}
