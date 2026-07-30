using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<int> Numeros = new List<int>();
    public PlayerPhase1 player;
    public GameObject animObject;
    public GameObject tutorialPanelObj;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerPhase1>();
        string sceneName = SceneManager.GetActiveScene().name;

        if (tutorialPanelObj != null)
        {
            tutorialPanelObj.SetActive(sceneName == "Fase 1");
        }
        if (animObject != null)
        {
            animObject.SetActive(false);
        }

    }

    public void OnButtonPlayClick()
    {
        Debug.Log("Start");
        player.StartCoroutine(Timer());
    }
    public void OnTutorialExitClick()
    {
        tutorialPanelObj.SetActive(false);
        animObject.SetActive(true); 
    }


    IEnumerator Timer()
    {
        foreach (int movimento in Numeros)
        {
            yield return StartCoroutine(player.MoveCharacter(movimento));
        }
    }
}
