using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElfsCtrl : MonoBehaviour
{
	private PlayerElfsPool soliderPool;
	public GameObject EnemyDestination;

	private List<SoliderElf> existList;

    // Start is called before the first frame update
    public void Init()
    {
		soliderPool = GetComponent<PlayerElfsPool>();
		soliderPool.Init();

		existList = new List<SoliderElf>();
    }


	public void GenerateNewSolider(Vector3 curPos) {

		var tempComp = soliderPool.PopObj();

		tempComp.transform.SetParent(soliderPool.parent.transform);
		tempComp.transform.localScale = Vector3.one;
		tempComp.transform.localEulerAngles = new Vector3(0, 0, 0);
		tempComp.transform.localPosition = new Vector3(curPos.x, curPos.y, curPos.z + 8f);

		if (!tempComp.isInit)
			tempComp.Init(this);
		else
			tempComp.BornedStage();

		existList.Add(tempComp);
	}

	private void Update()
	{
		Recycle();
	}

	private void Recycle ()
	{
		for (int i = 0; i < existList.Count; i++) {
			var solider = existList[i];
			if (solider.isDead) {
				soliderPool.PushObj(solider);
				existList.Remove(solider);
			}
		}
	}
}
