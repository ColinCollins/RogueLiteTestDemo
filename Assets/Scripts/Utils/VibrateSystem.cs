using UnityEngine;

public class VibrateSystem {

	// 是否开启振动
	private bool isOpenVibrate = false;
	public bool vibrateAvailable {
		get {
			return isOpenVibrate;
		}
		set {
			isOpenVibrate = value;

#if UNITY_ANDROID && !UNITY_EDITOR
			if (isOpenVibrate == false) {
				Cancel();
			}
#endif
			Debug.Log("VibrateSystem State: " + isOpenVibrate);
		}
	}

	private static VibrateSystem instance = null;
	public static VibrateSystem getInstance() {
		if (instance == null) {
			Debug.Log("VibrateSystem init.");
			instance = new VibrateSystem();
		}
		return instance;
	}

	public void Init() {
		instance = this;
		// vibrateAvailable = true;
		// 检测当前是否含有振动模块
		vibrateAvailable = HasVibrator();
	}

	public void VibrateOnce(long milliseconds = 0) {
		if (isOpenVibrate) {
#if UNITY_ANDROID && !UNITY_EDITOR
			if (milliseconds <= 0) 
				Vibrate();
			else {
				Vibrate(milliseconds);
			}
#elif UNITY_EDITOR
			Vibrate();
#endif
		}
	}

#if UNITY_ANDROID && !UNITY_EDITOR

	private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	private static AndroidJavaObject vibrator =currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
	private static AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

	///<summary>
	/// Only on Android
	/// https://developer.android.com/reference/android/os/Vibrator.html#vibrate(long)
	///</summary>
	public void Vibrate(long milliseconds)
	{
		vibrator.Call("vibrate", milliseconds);
	}

	///<summary>
	///Only on Android
	///</summary>
	public void Cancel()
	{
		vibrator.Call("cancel");
	}
#endif

	private bool HasVibrator() {
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context");
		string Context_VIBRATOR_SERVICE = contextClass.GetStatic<string>("VIBRATOR_SERVICE");
		AndroidJavaObject systemService = context.Call<AndroidJavaObject>("getSystemService", Context_VIBRATOR_SERVICE);
		if (systemService.Call<bool>("hasVibrator")) {
			return true;
		}
		else {
			Debug.LogWarning("VibrateSystem: Can't fint Vibrator on this platform.");
			return false;
		}
#elif UNITY_IOS && !UNITY_EDITOR
        return _HasVibrator ();
#else
		Debug.LogWarning("VibrateSystem: Can't fint Vibrator on this platform.");
		return false;
#endif
	}

	private void Vibrate() {
#if UNITY_EDITOR
		Debug.Log("Bzzzt! Cool vibration!");
#elif UNITY_ANDROID || UNITY_IPHONE
		Handheld.Vibrate();
#endif
	}
}
