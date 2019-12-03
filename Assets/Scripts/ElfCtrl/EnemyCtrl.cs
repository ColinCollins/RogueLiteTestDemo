using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
	public EnemyElfsCtrl EEPool;
	private EnemyGenerateModel generateCollection;

	public int life = 10;
	public int Life
	{
		set
		{
			life = value;
			UISystem.GetInstance().UpdateGamePanelData();
			if (life <= 0) {
				GameManager.GetInstance().PassToNextLevel();
			}
			
		}
		get
		{
			return life;
		}
	}

	private CenterCtrl manager;

	public void Init(CenterCtrl manager)
	{
		this.manager = manager;
		EEPool.Init();

		generateCollection = EEPool.transform.GetComponent<EnemyGenerateModel>();
		generateCollection.Init(this);
		generateCollection.SwitchGenerateModel(EnemyGenerateModelType.Random);

	}

	public void ResetScene() {
		Life = GameManager.GetInstance().CurLevel;
		EEPool.Recycle(true);
	}

	private void Update()
	{
		
	}

	public void GenerateEnemy (Vector3 pos)
	{
		EEPool.GenerateNewSolider(pos);
	}
}
