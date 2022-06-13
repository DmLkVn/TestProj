using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    Button[] buttons;
    bool disconnection;
    public static UnityEvent animand = new UnityEvent();
    public void PlayAnimation(string statename)
    {
        animator.Play(statename);
        if (statename == "Screen2" || statename == "Screen1")
        {
            ButtonLocker();
        }
    }
    private void ButtonLocker()
    {
        for (int i=0;i<buttons.Length;i++)
        {
            if (buttons[i].enabled)
            {
                buttons[i].enabled = false;
            }
            else
            {
                buttons[i].enabled = true;
            }
        }
    }

    private void DowloadAnimationEnd()
    {
        animand?.Invoke();
    }
    private void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        animator = GetComponent<Animator>();
        PlayAnimation("Fading");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable && !disconnection)
        {
            animator.Play("ConnectionAllert");
            disconnection = true;
        }
        else if (Application.internetReachability != NetworkReachability.NotReachable && disconnection)
        {
            animator.Play("ConnectionAllertRev");
            disconnection = false;
        }
    }
}
