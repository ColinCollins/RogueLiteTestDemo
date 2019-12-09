using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMasterAttackCtrl : MonoBehaviour
{
	// temporary
	private bool isMagic = false;
	public bool IsMagic {
		get {
			return isMagic;
		}
		set {
			isMagic = value;

		}
	}

	public float energyCast = 0;


	public GameObject Arrow;
	public GameObject LightningBall;

	private CallMasterElf manager;

    // Start is called before the first frame update
    public void Init(CallMasterElf manager)
    {
		this.manager = manager;
    }

	public void ShotAttack(Vector3 pos) {
		GameObject prefab = isMagic ? LightningBall : Arrow;
		GameObject tempObj = Instantiate(prefab);

		tempObj.transform.SetParent(manager.transform.parent);
		tempObj.transform.localScale = Vector3.one;
		tempObj.transform.localPosition = pos;
		tempObj.transform.localEulerAngles = Vector3.zero;
	}

}
