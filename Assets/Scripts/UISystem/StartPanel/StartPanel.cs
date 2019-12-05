using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : UIPanel
{	
	public void StartGame ()
	{
		GameManager.GetInstance().State = GameManager.GameState.Playing;
		manager.ShowSpecialPanel(PanelType.GamePanel);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
