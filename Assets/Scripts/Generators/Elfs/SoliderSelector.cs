using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoliderSelector : Selector
{
	private Transform attackRange;
	private SoliderElf owner;

	public float appearTime = 0.4f;
	public static SoliderSelector curHandle;

	public void Init(SoliderElf owner)
	{
		this.owner = owner;
		attackRange = transform.Find("AttackRange");
	}

	private void Update()
	{
		SelectedDetected();
	}

	public override void SelectedCallback()
	{
		base.SelectedCallback();

		if (curHandle != null) {
			curHandle.isSelected = false;
			curHandle.HideAttackRange();
		}

		curHandle = this;
		curHandle.isSelected = true;
		curHandle.ShowAttackRange();
	}

	public override void CancelSelectedCallback()
	{
		if (curHandle == null) return;

		curHandle.HideAttackRange();
		curHandle.isSelected = false;
		curHandle = null;
	}

	public void ShowAttackRange () {
		attackRange.DOScale(Vector3.one * owner.AttackRange, appearTime);
	}

	public void HideAttackRange () {
		attackRange.DOScale(new Vector3(0, 0, 0), appearTime);
	}
}
