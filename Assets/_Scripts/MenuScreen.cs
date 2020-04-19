using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour {


    [SerializeField]
    private CanvasGroup fadeGroup;

    [SerializeField] private float fadeInSpeed = 1f;

    public string LoadSceneName = "";

	// Use this for initialization
	private void Start ()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 1;
    }

    // Update is called once per frame
    void Update () {
        //FadeIn
            fadeGroup.alpha = 1 - Time.timeSinceLevelLoad*fadeInSpeed;


        if(LoadSceneName != "" && fadeGroup.alpha == 0)
        {
            SceneManager.LoadScene(LoadSceneName);
        }

    }
}
