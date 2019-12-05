                                                                                                                                    using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
	#region GameState
	public enum GameState {
		// Playing
		Playing,
		// StartMenu or Settle
		Pause,
		// Failed 
		Failed
	}

	#endregion

	private CenterCtrl centerCtrl;
	private VibrateSystem vibrateSys;
	#region UI 

	private UISystem uiSystem;

	#endregion 

	#region Single

	private static GameManager instance;
	public static GameManager GetInstance() {
		return instance;
	}

	#endregion

	private GameState state = GameState.Pause;
	public GameState State
	{
		get {
			return state;
		}
		set {
			state = value;
			if (state == GameState.Pause)
				Time.timeScale = 0;

			if (state == GameState.Playing)
				Time.timeScale = 1;
		}
	}

	public int CurLevel = 1;

	void Awake() {
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		centerCtrl = GetComponent<CenterCtrl>();
		centerCtrl.Init(this);

		uiSystem = GetComponent<UISystem>();
		uiSystem.Init(this);

		vibrateSys = new VibrateSystem();
		vibrateSys.Init();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public bool isPlaying() {

		return State == GameState.Playing;
	}

	public void PassToNextLevel() {
		uiSystem.ShowSpecialPanel(PanelType.SettlePanel);
	}

	public void GameOver() {
		State = GameState.Failed;
		uiSystem.ShowSpecialPanel(PanelType.SettlePanel);
	}
}
