
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [System.Serializable]
    public class OnButtonPressEvent : UnityEvent { }
    public OnButtonPressEvent OnButtonPressed;

    [SerializeField] private GameObject pad;
    [SerializeField] private List<Collider2D> recentPushers = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            if (recentPushers.Count == 0)
            {
                Invoke(nameof(ButtonPress), 0.2f);
            }
            recentPushers.Add(collision);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (recentPushers.Count > 0 && recentPushers.Contains(collision))
        {
            recentPushers.Remove(collision);

            if (recentPushers.Count == 0)
                Invoke(nameof(ButtonPress), 0.2f);
        }
    }

    public void ButtonPress()
    {
            transform.GetComponent<BoxCollider2D>().enabled = !transform.GetComponent<BoxCollider2D>().isActiveAndEnabled;
            pad.SetActive(!pad.activeSelf);
            OnButtonPressed.Invoke();
            AudioManager.Instance.PlaySound("buttonPressed");
    }

 
}
