using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettlePanel : UIPanel
{
	public Text btnText1;
	public Text btnText2;
	public Text btnText3;

	private PassRewardChoice handle1;
	private PassRewardChoice handle2;
	private PassRewardChoice handle3;

	private Dictionary<int, PassRewardChoice> rewardDic = null;

	public override void Init(UISystem manager)
	{
		base.Init(manager);

		rewardDic = new Dictionary<int, PassRewardChoice>();
		rewardDic.Add(0, PassRewardChoice.GetAttackUp());
		rewardDic.Add(1, PassRewardChoice.GetAttackRangeUp());
		rewardDic.Add(2, PassRewardChoice.GetCDSpeedUp());
		rewardDic.Add(3, PassRewardChoice.GetMovingSpeedUp());
	}

	public override void UpdateData()
	{
		base.UpdateData();

		// temporary
		int[] rewards = getRewards();
		handle1 = rewardDic[rewards[0]];
		handle2 = rewardDic[rewards[1]];
		handle3 = rewardDic[rewards[2]];

		btnText1.text = handle1.RewardText;
		btnText2.text = handle2.RewardText;
		btnText3.text = handle3.RewardText;
	}

	private int[] getRewards() {
		int random = Random.Range(0, 4);
		int[] rewards = new int[3];
		int j = 0;
		for (int i = 0; i < rewardDic.Count; i++)
		{
			if (i == random) continue;
			rewards[j] = i;
			j++;
		}

		return rewards;
	}

	public void Choice1() {
		handle1.RewardMethod();
		PassToNextLevel();
	}

	public void Choice2() {
		handle2.RewardMethod();
		PassToNextLevel();
	}

	public void Choice3() {
		handle3.RewardMethod();
		PassToNextLevel();
	}

	private void PassToNextLevel() {
		manager.ShowSpecialPanel(PanelType.GamePanel);
		manager.ClosePopup();
		CenterCtrl.GetInstance().ResetScene();
		GameManager.GetInstance().CurLevel++;
	}
}
