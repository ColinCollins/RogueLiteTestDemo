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
	public PlayerElfsCtrl PEPool;

	// CD 恢复速度
	public float RestoreSpeed = 10;
	public float EnergyCast = 20;
	public float MaxEnergyCount = 100;

	#endregion

	#region LocateObj

	public GameObject LocateObj;
	public float LocateMovingTime = 2.0f;
	private float movingRange = 30;
	private Vector3 initLocatePos = new Vector3(0, 0, -68);

	#endregion

	#region For GamePanel

	private float curEnergyCount;
	public float CurEnergyCount
	{
		set
		{
			curEnergyCount = value;
			UISystem.GetInstance().UpdateGamePanelData();
		}
		get
		{
			return curEnergyCount;
		}
	}

	#endregion

	#region GameFlower

	private CenterCtrl manager;

	// temporary
	public int life = 10;
	public int Life {
		get {
			return life;
		}

		set {
			life = value;	

			if (life <= 0) {
				GameManager.GetInstance().GameOver();
			}
		}
	}

	#endregion

	private bool isTouchDown = false;

	// Start is called before the first frame update
	public void Init(CenterCtrl manager)
	{
		this.manager = manager;
		curEnergyCount = MaxEnergyCount;
		LocateObj.transform.localPosition = initLocatePos;

		PEPool.Init();

		// LocateObjMoving();
	}

	public void ResetScene()
	{
		Life = 10;
		CurEnergyCount = MaxEnergyCount;

		PEPool.Recycle(true);
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.GetInstance().isPlaying())
		{
			restoreEnergySlider();
			clickToGenerateDetection();

			switchDOTween(true);
		}
		else {
			switchDOTween(false);
		}
	}

	private void clickToGenerateDetection()
	{
//#if UNITY_EDITOR
//		if (Input.GetMouseButtonDown(0))
//		{
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit;
//			if (Physics.Raycast(ray, out hit, 1000f, rayLayer))
//			{
//				// play Animation
//				clickObjAnim.Play();
//				// Generate
//				generateNewSolider();
//			}
//		}
//#endif
//#if UNITY_ANDROID
//		if (Input.touchCount > 0 && !isTouchDown)
//		{
//			isTouchDown = true;

//			var touch = Input.GetTouch(0);
//			Ray ray = Camera.main.ScreenPointToRay(touch.position);
//			RaycastHit hit;
//			if (Physics.Raycast(ray, out hit, 1000f, rayLayer))
//			{
//				// play Animation
//				clickObjAnim.Play();
//				// Generate
//				generateNewSolider();
//			}
//		}

//		if (Input.touchCount <= 0) isTouchDown = false;
//#endif
	}

	public void LocateObjMoving()
	{
		Sequence moving = DOTween.Sequence();
		moving.Append(LocateObj.transform.DOMoveX(-movingRange, 0f));
		moving.Append(LocateObj.transform.DOMoveX(movingRange, LocateMovingTime).SetEase(Ease.Linear));

		moving.AppendCallback(() =>
		{
			LocateObjMoving();
		});
	}

	public void switchDOTween(bool isOpen) {
		if (isOpen && DOTween.timeScale <= 0) DOTween.timeScale = 1;
		if (!isOpen && DOTween.timeScale > 0) DOTween.timeScale = 0;
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
		else {
			CurEnergyCount = MaxEnergyCount;
		}
	}

	public void GenerateNewSolider()
	{
		var pos = LocateObj.transform.localPosition;
		var Lx = pos.x;

		if (curEnergyCount - EnergyCast < 0 || Lx <= -(movingRange - 3) || Lx >= (movingRange - 3))
		{
			return;
		};

		curEnergyCount -= EnergyCast;
		PEPool.GenerateNewSolider(pos);
	}
}
