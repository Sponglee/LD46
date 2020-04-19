using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    public Transform playerReference;
    public Transform lilPlayerReference;
    public Transform gemReference;

    public bool GameOverBool = false;

    public CinemachineVirtualCameraBase eatenCam;

    public Text gemText;
    public bool GemGrabbed = false;
    public Transform gemPanel;

    public void RestartScene()
    {
        SceneManager.LoadScene("Main");
    }

  

    public void GlowGemPanel()
    {
        gemPanel.GetComponent<Animator>().SetTrigger("PanelGlow");
    }

    public void GrabGem(Transform gem)
    {
        GemGrabbed = true;
        gemText.text = "1/1";
    }

    public void DropGem(Transform gem)
    {
        GemGrabbed = false;
        gemText.text = "0/1";
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


}
