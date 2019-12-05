using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : UIPanel
{
	public override void Init(UISystem manager)
	{
		base.Init(manager);
	}


	public override void UpdateData()
	{
		base.UpdateData();
	}

	public void GameReset() {
		SceneManager.LoadScene("GameScene");
	}
}
