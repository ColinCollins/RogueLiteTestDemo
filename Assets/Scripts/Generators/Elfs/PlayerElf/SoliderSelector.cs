using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoliderSelector : Selector
{
	public Transform attackRange;
	public Transform detectRange;

	private SoliderElf owner;

	public float appearTime = 0.4f;
	public static SoliderSelector curHandle;

	public void Init(SoliderElf owner)
	{
		this.owner = owner;
	}

	private void Update()
	{
		SelectedDetected();
	}

	public override void SelectedCallback(Selector handle)
	{
		base.SelectedCallback(handle);

		if (curHandle != null) {
			curHandle.isSelected = false;
			curHandle.HideAttackRange();
		}

		curHandle = handle as SoliderSelector;
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
		detectRange.DOScale(Vector3.one * owner.DetectRange, appearTime);
	}

	public void HideAttackRange () {
		attackRange.DOScale(new Vector3(0, 0, 0), appearTime);
		detectRange.DOScale(new Vector3(0, 0, 0), appearTime);
	}
}
