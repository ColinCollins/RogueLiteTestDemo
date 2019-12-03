using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
	protected UISystem manager;

	public PanelType Type;
	
	// control the panel sort
	public int weight = 0;

	// panel display type
	public bool isPopup = false;

	public virtual void Init(UISystem manager) {
		this.manager = manager;
	}

	public virtual void UpdateData() { }
}
