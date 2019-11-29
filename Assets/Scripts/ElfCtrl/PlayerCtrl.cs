using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCtrl : MonoBehaviour
{
	#region Properties union

	public LayerMask rayLayer;
	public Animation clickObjAnim;
	public PlayerElfsCtrl playerElfsCtrl;

	// CD 恢复速度
	public float RestoreSpeed = 10;
	public float EnergyCast = 20;

	#endregion

	#region LocateObj

	public GameObject LocateObj;
	public float LocateMovingTime = 2.0f;
	private float movingRange = 50;
	private Vector3 initLocatePos = new Vector3(0, 3, -68);

	#endregion

	#region UI

	public Text EnergyText;
	public Slider EnergySlider;
	public float MaxEnergyCount = 100;

	private float curEnergyCount;
	public float CurEnergyCount
	{
		set
		{
			curEnergyCount = value;
			EnergySlider.value = curEnergyCount / MaxEnergyCount;
			EnergyText.text = string.Format("{0}", Convert.ToInt32(curEnergyCount));
		}
		get
		{
			return curEnergyCount;
		}
	}

	#endregion

	// Start is called before the first frame update
	public void Init()
	{
		curEnergyCount = MaxEnergyCount;
		LocateObj.transform.localPosition = initLocatePos;

		playerElfsCtrl.Init();
		// start to moving
		locateObjMoving();
	}

	// Update is called once per frame
	void Update()
	{
		restoreEnergySlider();
		clickToGenerateDetection();
	}

	private void clickToGenerateDetection()
	{
#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000f, rayLayer))
			{
				// play Animation
				clickObjAnim.Play();
				// Generate
				generateNewSolider();
			}
		}
#endif
	}

	private void locateObjMoving()
	{
		Sequence moving = DOTween.Sequence();
		moving.Append(LocateObj.transform.DOMoveX(-movingRange, 0f));
		moving.Append(LocateObj.transform.DOMoveX(movingRange, LocateMovingTime).SetEase(Ease.Linear));

		moving.AppendCallback(() =>
		{
			locateObjMoving();
		});
	}

	public void ResetGenerator()
	{
		CurEnergyCount = MaxEnergyCount;

	}

	private void restoreEnergySlider()
	{
		if (CurEnergyCount < MaxEnergyCount)
		{
			CurEnergyCount += RestoreSpeed * Time.deltaTime;
		}
	}

	private void generateNewSolider()
	{
		var pos = LocateObj.transform.localPosition;
		var Lx = pos.x;

		if (curEnergyCount - EnergyCast < 0 || Lx <= -(movingRange - 2) || Lx >= (movingRange - 2))
		{
			return;
		};

		curEnergyCount -= EnergyCast;
		playerElfsCtrl.GenerateNewSolider(pos);
	}
}
