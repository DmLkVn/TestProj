using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneSwitcher : MonoBehaviour
{
    private AsyncOperation loadSceneAsync;
    GameManager gameManager;

    [HideInInspector] public bool musicmute;
    [HideInInspector] public bool sfxmute;
    public static UnityEvent SceneSwitch = new UnityEvent();

    private void Awake()
    {
        AnimatorController.animand.AddListener(OnAnimationEnd);
        Application.targetFrameRate = 60;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SceneTransition(string scenename)
    {
        if (scenename == "2ndScene")
        {
            loadSceneAsync = SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Additive);
            loadSceneAsync.allowSceneActivation = false;
        }
        else 
        {
            loadSceneAsync = SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Single);
            loadSceneAsync.allowSceneActivation = false;
        }
    }

    public void OnAnimationEnd()
    {
        if (SceneManager.GetActiveScene().name == "2ndScene")
        {
            SceneSwitch?.Invoke();
        }
        loadSceneAsync.allowSceneActivation = true;
    }
}
