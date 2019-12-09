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
	public float CallEnergyCast = 20;
	public float WeaponEnergyCast = 30;

	public float MaxCallEnergyCount = 100;
	public float MaxWeaponEnergyCount = 100;

	// For GamePanel
	private float curCallEnergyCount;
	public float CurCallEnergyCount
	{
		set
		{
			curCallEnergyCount = value;
			UISystem.GetInstance().UpdateGamePanelData();
		}
		get
		{
			return curCallEnergyCount;
		}
	}

	private float curWeaponEnergyCount;
	public float CurWeaponEnergyCount {
		get {
			return curWeaponEnergyCount;
		}

		set {
			curWeaponEnergyCount = value;
			UISystem.GetInstance().UpdateGamePanelData();
		}
	}

	#endregion

	#region CallMaster 

	public GameObject CallMasterPrefab;
	private CallMasterElf callMaster;

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
		curCallEnergyCount = MaxCallEnergyCount;
		curWeaponEnergyCount = MaxWeaponEnergyCount;

		PEPool.Init();

		initCallMaster();

		// LocateObjMoving(); temporay removes
	}

	public void ResetScene()
	{
		Life = 10;
		CurCallEnergyCount = MaxCallEnergyCount;
		CurWeaponEnergyCount = MaxWeaponEnergyCount;

		PEPool.Recycle(true);
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.GetInstance().isPlaying())
		{
			restoreEnergySlider();
			// clickToGenerateDetection();

			switchDOTween(true);
		}
		else {
			switchDOTween(false);
		}
	}

	#region Call Master 

	private void initCallMaster()
	{
		var obj = Instantiate(CallMasterPrefab);
		var tempTrans = obj.transform;
		tempTrans.SetParent(PEPool.transform);
		tempTrans.localScale = Vector3.one;
		tempTrans.localEulerAngles = Vector3.zero;

		callMaster = obj.GetComponent<CallMasterElf>();
		callMaster.Init();
	}

	public CallMasterElf GetCallMaster() {
		return callMaster;
	}

	#endregion

	//	private void clickToGenerateDetection()
	//	{
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
	//	}

	//public void LocateObjMoving()
	//{
	//	Sequence moving = DOTween.Sequence();
	//	moving.Append(LocateObj.transform.DOMoveX(-movingRange, 0f));
	//	moving.Append(LocateObj.transform.DOMoveX(movingRange, LocateMovingTime).SetEase(Ease.Linear));

	//	moving.AppendCallback(() =>
	//	{
	//		LocateObjMoving();
	//	});
	//}

	public void switchDOTween(bool isOpen) {
		if (isOpen && DOTween.timeScale <= 0) DOTween.timeScale = 1;
		if (!isOpen && DOTween.timeScale > 0) DOTween.timeScale = 0;
	}

	private void restoreEnergySlider()
	{
		// restore summon energy
		if (CurCallEnergyCount < MaxCallEnergyCount)
		{
			CurCallEnergyCount += RestoreSpeed * Time.deltaTime;
		}
		else {
			CurCallEnergyCount = MaxCallEnergyCount;
		}

		// restore weapon energy
		if (CurWeaponEnergyCount < MaxWeaponEnergyCount)
		{
			CurWeaponEnergyCount += RestoreSpeed * Time.deltaTime;
		}
		else
		{
			CurWeaponEnergyCount = MaxWeaponEnergyCount;
		}
	}

	public void GenerateNewSolider()
	{
		if (curCallEnergyCount - CallEnergyCast < 0) return;

		CurCallEnergyCount -= CallEnergyCast;
		PEPool.GenerateNewSolider(callMaster.transform.localPosition);
	}


	public void CallMasterAttack() {
		float energyCast = callMaster.AttackCtrl.IsMagic ? 20f : 15f;
		if (curWeaponEnergyCount - energyCast < 0) return;

		CurWeaponEnergyCount -= energyCast;
		callMaster.ShotAttack();
	}

}
