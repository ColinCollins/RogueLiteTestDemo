using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCover : MonoBehaviour
{
	private Animator anim;

	public delegate void AnimFinishedCallback();
	public AnimFinishedCallback callback;

	public void Init (Animator animator)
	{
		anim = animator;
	}

	public void PlaySpecialAnim(string AnimName, AnimFinishedCallback callbackHandle = null)
	{
		// Debug.Log("Play Animation" + AnimName);
		StartCoroutine(WaitingForFinished(AnimName, callbackHandle));
	}

	private IEnumerator WaitingForFinished(string AnimName, AnimFinishedCallback callback) {
		anim.SetTrigger(AnimName);

		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

		while (!info.IsName(AnimName) || info.normalizedTime < 1.0f)
		{
			info = anim.GetCurrentAnimatorStateInfo(0);
			yield return null;
		}

		if (callback != null) {
			callback();
		}
	}
}
