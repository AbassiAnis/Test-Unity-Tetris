using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject introPanel;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject editPanel;
    [SerializeField]
    private GameObject inGameHeaderPanel;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.GameEnded += ShowGameOverPanel;
        InitPanels();
    }

    private void OnDestroy()
    {
        GameManager.GameEnded -= ShowGameOverPanel;
    }


    public void StartGame()
    {
        introPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        editPanel.SetActive(false);
        inGameHeaderPanel.SetActive(true);
        GameManager.Instance.StartGame();
    }

    private void InitPanels()
    {
        introPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        editPanel.SetActive(false);
        inGameHeaderPanel.SetActive(false);

    }


    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        introPanel.SetActive(false);
        editPanel.SetActive(false);
        inGameHeaderPanel.SetActive(false);
    }
}
