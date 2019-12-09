using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyGenerateModel : MonoBehaviour
{
	private EnemyCtrl manager;

	public delegate void GenerateHandle();
	public GenerateHandle handle;

	public GameObject GeneratePoint1;

	public float MaxIntervalTime = 0.5f;
	private float curIntervalTime = 0;
	private float model1GenerateRange = 6;

	public void Init(EnemyCtrl manager)
	{
		this.manager = manager;
		handle = null;
	}

	public void SwitchGenerateModel(EnemyGenerateModelType type) {
		switch (type) {
			case EnemyGenerateModelType.Random:
				handle = GenerateModel1;
				break;
			default:
				handle = null;
				break;
		}
	}

	private void GenerateModel1 ()
	{
		curIntervalTime += Time.deltaTime;

		if (curIntervalTime >= MaxIntervalTime) {
			float posX = Random.Range(-model1GenerateRange, model1GenerateRange);
			GeneratePoint1.transform.DOLocalMoveX(posX, 0f);
			curIntervalTime = 0;
			manager.GenerateEnemy(GeneratePoint1.transform.localPosition);
		}
	}
}
