using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
	public LayerMask layer;
	public bool isSelected = false;
	private bool isTouchDown = false;

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
#if UNITY_ANDROID
		if (Input.touchCount > 0 && !isTouchDown)
		{
			isTouchDown = true;
			var touch = Input.GetTouch(0);

			Ray ray = Camera.main.ScreenPointToRay(touch.position);
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

		if (Input.touchCount <= 0) isTouchDown = false;

#endif
	}

	public virtual void SelectedCallback(Selector handle) { }

	public virtual void CancelSelectedCallback() { }
}
