using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	#region GameState
	public enum GameState {
		// Playing
		GamePlaying,
		// StartMenu or Settle
		GamePause,
		// Failed 
		GameFailed
	}

	#endregion

	#region Generator Ctrl

	private PlayerCtrl playerCtrl;

	#endregion

	// Start is called before the first frame update
	void Start()
    {
		playerCtrl = GetComponent<PlayerCtrl>();
		playerCtrl.Init();

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
