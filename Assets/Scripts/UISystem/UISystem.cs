using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelType
{
	StartPanel,
	GamePanel,
	SettlePanel,
	GameOverPanel
}

public class UISystem : MonoBehaviour
{

	#region UI Ctrl

	public GameObject mainCanvas;

	public GameObject SettlePanel;
	public GameObject GamePanel;
	public GameObject StartPanel;
	public GameObject GameOverPanel;

	#endregion

	#region Grocery
	
	// for gamePanel
	public GameObject ClickObject;

	#endregion

	private Queue<UIPanel> popUpQueue = null;
	private List<UIPanel> panelList = null;

	private GameManager manager = null;
	private UIPanel curPanelHandle = null;

	private SettlePanel hSettlePanel;
	private GamePanel hGamePanel;
	private StartPanel hStartPanel;
	private GameOverPanel hGameOverPanel;

	#region Single

	private static UISystem instance;
	public static UISystem GetInstance()
	{
		return instance;
	}

	#endregion


	public void Init(GameManager manager)
	{
		this.manager = manager;
		instance = this;

		panelList = new List<UIPanel>();
		popUpQueue = new Queue<UIPanel>();

		hStartPanel = initPanel(StartPanel, PanelType.StartPanel) as StartPanel;
		hSettlePanel = initPanel(SettlePanel, PanelType.SettlePanel) as SettlePanel;
		hGamePanel = initPanel(GamePanel, PanelType.GamePanel) as GamePanel;
		hGameOverPanel = initPanel(GameOverPanel, PanelType.GameOverPanel) as GameOverPanel;

		SortPanel();

		ShowSpecialPanel(PanelType.StartPanel);
	}

	private UIPanel initPanel(GameObject prefab, PanelType type)
	{
		GameObject panel = Instantiate(prefab);
		UIPanel comp = null;

		switch (type)
		{
			case PanelType.StartPanel:
				comp = panel.GetComponent<StartPanel>();
				break;
			case PanelType.SettlePanel:
				comp = panel.GetComponent<SettlePanel>();
				break;
			case PanelType.GamePanel:
				comp = panel.GetComponent<GamePanel>();
				break;
			case PanelType.GameOverPanel:
				comp = panel.GetComponent<GameOverPanel>();
				break;
			default: break;
		}

		panel.SetActive(false);

		comp.Init(this);
		panelList.Add(comp);

		return comp;
	}

	private void SortPanel()
	{
		panelList.Sort((a, b) =>
		{
			return a.weight - b.weight;
		});

		for (int i = 0; i < panelList.Count; i++)
		{
			var trans = panelList[i].transform;
			trans.SetParent(mainCanvas.transform);
			trans.localPosition = Vector3.zero;
			trans.localEulerAngles = Vector3.zero;
			trans.localScale = Vector3.one;
			trans.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
		}
	}

	public void ShowSpecialPanel(PanelType type, bool isUpdate = true)
	{
		var panel = FindSpecialPanel(type);

		if (type != PanelType.GamePanel)
			manager.State = GameManager.GameState.Pause;
		else
			manager.State = GameManager.GameState.Playing;

		if (panel.isPopup)
		{
			if (popUpQueue.Contains(panel))
			{
				Debug.Log("UISystem get Error");
				return;
			}

			popUpQueue.Enqueue(panel);
			panel.gameObject.SetActive(true);
		}
		else
		{
			// replace
			if (curPanelHandle != null)
			{
				curPanelHandle.gameObject.SetActive(false);
				// special 
				// SwitchClickObject(false);
			}

			curPanelHandle = panel;
			curPanelHandle.gameObject.SetActive(true);

			// SwitchClickObject(true);
		}

		if (isUpdate)
		{
			panel.UpdateData();
		}
	}

	public void ClosePopup(bool isFlushPanel = false)
	{
		if (this.popUpQueue.Count <= 0) return;

		var panel = this.popUpQueue.Dequeue();
		panel.gameObject.SetActive(false);

		if (curPanelHandle.Type == PanelType.GamePanel && popUpQueue.Count <= 0)
			manager.State = GameManager.GameState.Playing;

		if (isFlushPanel && this.curPanelHandle != null)
		{
			this.curPanelHandle.UpdateData();
		}
	}

	public void SwitchClickObject(bool isShow)
	{
		if (isShow)
		{
			if (curPanelHandle.Type == PanelType.GamePanel)
				ClickObject.SetActive(true);
		}
		else
		{
			if (curPanelHandle == null || curPanelHandle.Type != PanelType.GamePanel)
				ClickObject.SetActive(false);
		}
	}

	public UIPanel FindSpecialPanel(PanelType type)
	{
		for (int i = 0; i < panelList.Count; i++)
		{
			var panel = panelList[i];
			if (panelList[i].Type == type)
				return panel;
		}

		return null;
	}

	public void UpdateGamePanelData()
	{
		// gamePanel.
		hGamePanel.UpdateData();
	}
}
