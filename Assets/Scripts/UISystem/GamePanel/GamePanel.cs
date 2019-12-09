using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class GamePanel : UIPanel
{
	private PlayerCtrl playerCtrl;
	private EnemyCtrl enemyCtrl;

	public Animator clickObjAnim;

	public Text CallEnergyValue;
	public Text WeaponEnergyValue;

	public Text EnemyLife;
	public Text PlayerLife;
	public Slider CallCDSlider;
	public Slider WeaponCDSlider;

	public MovingScrollBarCtrl MovingSBCtrl;

	public Toggle SwitchModel;
	public GameObject CallMagicBtn;
	public GameObject WeaponBtn;

	private bool isWeaponModel = false;
	private bool isChanging = false;

	public bool IsWeaponModel {
		get {
			return isWeaponModel;
		}
		set {
			if (isChanging) return;

			isWeaponModel = value;
			SwitchBtnState();
		}
	}

	public override void Init(UISystem manager)
	{
		base.Init(manager);

		playerCtrl = CenterCtrl.GetInstance().PCtrl;
		enemyCtrl = CenterCtrl.GetInstance().ECtrl;

		MovingSBCtrl.Init();
		MovingSBCtrl.ResetCallMasterTo(0.5f);

		CallMagicBtn.GetComponent<Button>().onClick.AddListener(() => {
			GenerateClick();
		});

		WeaponBtn.GetComponent<Button>().onClick.AddListener(() => {
			WeaponClick();
		});

		SwitchModel.onValueChanged.AddListener((value) => {
			IsWeaponModel = value;
		});
	}

	public override void UpdateData()
	{
		base.UpdateData();

		EnemyLife.text = string.Format("{0}", Convert.ToInt32(enemyCtrl.Life));
		PlayerLife.text = string.Format("{0}", Convert.ToInt32(playerCtrl.Life));

		CallCDSlider.value = playerCtrl.CurCallEnergyCount / playerCtrl.MaxCallEnergyCount;
		CallEnergyValue.text = string.Format("{0}", Convert.ToInt32(playerCtrl.CurCallEnergyCount));

		WeaponCDSlider.value = playerCtrl.CurWeaponEnergyCount / playerCtrl.MaxWeaponEnergyCount;
		WeaponEnergyValue.text = string.Format("{0}", Convert.ToInt32(playerCtrl.CurWeaponEnergyCount));
	}

	public void ResetScene() {
		SceneManager.LoadScene("GameScene");
	}

	public void WeaponClick()
	{
		if (!isWeaponModel)
		{
			// switch 
			IsWeaponModel = true;
			return;
		}

		VibrateSystem.getInstance().VibrateOnce(100);
		CenterCtrl.GetInstance().PCtrl.CallMasterAttack();
	}

	public void GenerateClick ()
	{
		// temporary hiden
		// clickObjAnim.SetTrigger("Click");

		if (isWeaponModel)
		{
			// switch
			IsWeaponModel = false;
			return;
		}

		VibrateSystem.getInstance().VibrateOnce(100);
		CenterCtrl.GetInstance().PCtrl.GenerateNewSolider();
	}

	// temporary
	public void WeaponStateSwitch() {
		CenterCtrl.GetInstance().PCtrl.GetCallMaster().AttackCtrl.IsMagic = 
			!CenterCtrl.GetInstance().PCtrl.GetCallMaster().AttackCtrl.IsMagic;
	}


	private void SwitchBtnState()
	{
		isChanging = true;

		var mainBtn = IsWeaponModel ? WeaponBtn : CallMagicBtn;
		var subBtn = IsWeaponModel ? CallMagicBtn : WeaponBtn;

		float duration = 0.3f;

		Sequence act1 = DOTween.Sequence();
		act1.Append(mainBtn.transform.DOScale(Vector3.one, duration));
		act1.Join(mainBtn.transform.DOLocalMove(new Vector3(0, -50, 0), duration));

		act1.Join(subBtn.transform.DOScale(Vector3.one * 0.6f, duration));
		act1.Join(subBtn.transform.DOLocalMove(new Vector3(200, -125, 0), duration));

		act1.AppendCallback(() => {
			isChanging = false;

			mainBtn.transform.SetSiblingIndex(-2);
			subBtn.transform.SetSiblingIndex(-1);
		});
	}

}
