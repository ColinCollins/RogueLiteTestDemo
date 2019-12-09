using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassRewardChoice
{
	public delegate void PassRewardHandle();

	public int maxReward = 2;
	public string RewardText = "";
	public PassRewardHandle RewardMethod;

	public PassRewardChoice(string text)
	{
		RewardText = text;
	}

	public static PassRewardChoice GetAttackRangeUp()
	{
		var tempHandle = new PassRewardChoice("Solider Attack Range Up +");
		tempHandle.RewardMethod = () =>
		{
			CenterCtrl.GetInstance().PCtrl.PEPool.SoliderElfAttackRange += 1;
		};

		return tempHandle;
	}

	public static PassRewardChoice GetAttackUp()
	{
		var tempHandle = new PassRewardChoice("Solider Attack Up +");
		tempHandle.RewardMethod = () =>
		{
			CenterCtrl.GetInstance().PCtrl.PEPool.SoliderElfAttack += 2;
		};

		return tempHandle;
	}

	public static PassRewardChoice GetCDSpeedUp()
	{
		var tempHandle = new PassRewardChoice("CD Speed Up +");
		tempHandle.RewardMethod = () =>
		{
			CenterCtrl.GetInstance().PCtrl.RestoreSpeed += 3;
		};

		return tempHandle;
	}

	public static PassRewardChoice GetMovingSpeedUp()
	{
		var tempHandle = new PassRewardChoice("Moving Speed Up +");
		tempHandle.RewardMethod = () =>
		{
			CenterCtrl.GetInstance().PCtrl.PEPool.SoliderElfMoveSpeed *= 1.5f;
		};

		return tempHandle;
	}
}
