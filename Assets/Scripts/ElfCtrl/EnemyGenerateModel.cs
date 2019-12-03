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

	public void Init(EnemyCtrl manager)
	{
		this.manager = manager;
		handle = null;
	}

	private void Update()
	{
		if (handle != null && GameManager.GetInstance().isPlaying()) {
			handle();
		}
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
			float posX = Random.Range(-48, 48);
			GeneratePoint1.transform.DOLocalMoveX(posX, 0f);
			manager.GenerateEnemy(GeneratePoint1.transform.localPosition);
			curIntervalTime = 0;
		}
	}
}
