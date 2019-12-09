using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovingScrollBarCtrl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
	private CallMasterElf callMaster;
	private Scrollbar handle;

	private bool isDrag = false;

	private float lastValue = 0;

    public void Init()
    {
		handle = GetComponent<Scrollbar>();
		callMaster = CenterCtrl.GetInstance().PCtrl.GetCallMaster();
    }

	public void ResetCallMasterTo(float value)
	{
		if (lastValue == value) return;

		handle.value = value;
		lastValue = value;
		// move to 
		callMaster.SetMovingTarget(value);
	}

	public void OnDrag(PointerEventData eventData)
	{
		// Debug.Log("Drag");

		if (lastValue == handle.value) return;

		lastValue = handle.value;
		// move to 
		callMaster.SetMovingTarget(lastValue);
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		isDrag = true;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		// Debug.Log("End Drag");
		isDrag = false;

		if (lastValue == handle.value)

		// move to 
		lastValue = handle.value;
		callMaster.SetMovingTarget(handle.value);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Pointer Down");
		if (isDrag) return;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("Pointer Up");
		if (lastValue == handle.value || isDrag) return;

		// move to 
		lastValue = handle.value;
		callMaster.SetMovingTarget(handle.value);
	}
}
