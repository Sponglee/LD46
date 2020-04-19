using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    public Text gemText;
    public bool GemGrabbed = false;
    public Transform gemPanel;

    public void RestartScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void CheckExit()
    {
        if(GemGrabbed)
        {
            Win();
        }
        else
        {
            GlowGemPanel();
        }
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

    public void Win()
    {
        SceneManager.LoadScene("Win");
    }


}
