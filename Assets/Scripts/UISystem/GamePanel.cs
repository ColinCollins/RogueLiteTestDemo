using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePanel : UIPanel
{
	private PlayerCtrl playerCtrl;
	private EnemyCtrl enemyCtrl;

	public Text EnergyValue;
	public Text EnemyLife;
	public Text PlayerLife;
	public Slider Energy;

	public override void Init(UISystem manager)
	{
		base.Init(manager);

		playerCtrl = CenterCtrl.GetInstance().PCtrl;
		enemyCtrl = CenterCtrl.GetInstance().ECtrl;
	}

	public override void UpdateData()
	{
		base.UpdateData();

		EnemyLife.text = string.Format("{0}", Convert.ToInt32(enemyCtrl.Life));
		PlayerLife.text = string.Format("{0}", Convert.ToInt32(playerCtrl.Life));
		Energy.value = playerCtrl.CurEnergyCount / playerCtrl.MaxEnergyCount;
		EnergyValue.text = string.Format("{0}", Convert.ToInt32(playerCtrl.CurEnergyCount));
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void ResetScene() {
		SceneManager.LoadScene("GamePanel");
	}
}
