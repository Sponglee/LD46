using UnityEngine;
using UnityEngine.UI;

public class PanelGlowUI : MonoBehaviour
{


    public Text panelText;
    
    private void Start()
    {
        GameManager.Instance.OnTaskMissing.AddListener(PanelGlow);
        GameManager.Instance.OnTaskChanged.AddListener(TargetChangedState);
    }

    public void PanelGlow(Transform targetPanel)
    {
        Debug.Log("GLOW");
        if(targetPanel.name == transform.name)
            transform.GetComponent<Animator>().SetTrigger("PanelGlow");
    }

    public void TargetChangedState(Transform target,bool isOn)
    {
        Debug.Log(isOn);
        Debug.Log(">"+target);
        if (target.name == transform.name)
            panelText.text = string.Format("{0}/1", isOn ? 1 : 0);
        else if(isOn)
            GameManager.Instance.GlowGemPanel(transform);

    }

    public void TargetNotOnExit(Transform target)
    {  
        panelText.text = "0/1";
    }


}
