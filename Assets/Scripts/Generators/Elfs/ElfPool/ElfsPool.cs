using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfsPool<T> : MonoBehaviour
{
	// public int MaxCount = 0;
	protected List<T> objList = null;

	public GameObject prefab;
	public GameObject parent;

	public virtual void Init()
	{
		objList = new List<T>();
	}

	public virtual T PopObj()
	{
		if (objList.Count > 0)
		{
			T comp = objList[0];
			objList.RemoveAt(0);

			Debug.Log("Pool pop");

			return comp;
		}
		else
		{
			GameObject tempObj = Instantiate(prefab);
			T comp = tempObj.GetComponent<T>();
			if (comp == null)
			{
				Debug.LogError(string.Format("{0} generate error.", this));

				return default(T);
			}

			return comp;
		}
	}

	public virtual void PushObj(T obj)
	{
		objList.Add(obj);
	}
}