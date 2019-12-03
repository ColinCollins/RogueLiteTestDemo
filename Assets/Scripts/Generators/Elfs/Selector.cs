using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
	public LayerMask layer;
	public bool isSelected = false;

	public virtual void SelectedDetected() {
#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000f, layer))
			{
				SelectedCallback(hit.transform.GetComponent<Selector>());
			}
			else
			{
				CancelSelectedCallback();
			}
		}
#endif
	}

	public virtual void SelectedCallback(Selector handle) { }

	public virtual void CancelSelectedCallback() { }
}
