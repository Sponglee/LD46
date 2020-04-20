using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [System.Serializable]
    public class OnTaskMissingEvent : UnityEvent<Transform> { }
    public OnTaskMissingEvent OnTaskMissing;

    [System.Serializable]
    public class OnTaskChangedEvent : UnityEvent<Transform, bool> { }
    public OnTaskChangedEvent OnTaskChanged;

    public Transform playerReference;
    public Transform lilPlayerReference;
    public Transform gemReference;

    public bool GameOverBool = false;

    public CinemachineVirtualCameraBase eatenCam;

    public Transform selectionCanvas;


    public bool GemGrabbed = false;
    public Transform gemPanel;
    public Transform lilPanel;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitToMenu();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("Main");
    }

  

    public void GlowGemPanel(Transform panelPanel)
    {
        OnTaskMissing.Invoke(panelPanel);
    }

    public void TargetInDoor(Transform targetPanel, bool isOn)
    {
        GemGrabbed = isOn;
        OnTaskChanged.Invoke(targetPanel, isOn);
    }

  

    public void WinSequence()
    {
        SetFocusCam(playerReference);
        Invoke(nameof(LoadWin), 2f);
    }

    public void LoadWin()
    {
        SceneManager.LoadScene("Win");
    }

    public void TargetEaten(Transform target)
    {
        //AudioManager.Instance.PlaySound("enemyHit");
        target.GetComponentInChildren<Animator>().Play("Death");
        GameOverBool = true;
        SetFocusCam(target);
        Invoke(nameof(RestartScene), 2f);
    }

    public void SetFocusCam(Transform target)
    {
        eatenCam.Priority = 99;
        eatenCam.Follow = target;
        eatenCam.LookAt = target;
    }

    public void MoveSelectionCanvas(Transform target)
    {
        selectionCanvas.gameObject.SetActive(true);
        selectionCanvas.position = target.position;
    }

    public void DisableSelectionCanvas()
    {
        selectionCanvas.gameObject.SetActive(false);
    }


    public void Quit()
    {
        Application.Quit();
    }
    
    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
