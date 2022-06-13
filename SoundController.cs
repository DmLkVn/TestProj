using UnityEngine;
using structs;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    public AudioClip Music;
    public AudioClip Button;
    public AudioClip coinSound;
    AudioSource audioSource;
    SceneSwitcher sceneSwitcher;
    bool mute;
    GameData gameData;
    string jsonpath;
    string filename = "data.json";


    private void Start()
    {
        Coin.scoreUp.AddListener(PlayCoinSound);
        audioSource = GetComponent<AudioSource>();
        sceneSwitcher = FindObjectOfType<SceneSwitcher>();

        if (Music != null)
        {
            audioSource.loop = true;
            audioSource.clip = Music;
            audioSource.Play();
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        jsonpath = Path.Combine(Application.persistentDataPath, filename);
#else
        jsonpath = Path.Combine(Application.dataPath, filename);
#endif
        MuteChecker();
    }

    private void MuteChecker()
    {
        if (File.Exists(jsonpath))
        {
            string json = File.ReadAllText(jsonpath);
            gameData = JsonUtility.FromJson<GameData>(json);

            if (Music != null)
            {
                audioSource.mute = !gameData.musicmute;
            }

            else if (coinSound != null || Button != null)
            {
                audioSource.mute = !gameData.sfxmute;
            }
        }
    }
    public void ButtonSound()
    {
        if (Button.isReadyToPlay && audioSource !=null)
        {
            audioSource.clip = Button;
            audioSource.Play();
        }
    }

    public void PlayCoinSound()
    {
        if (coinSound != null)
        {
            audioSource.clip = coinSound;
            audioSource.Play();
        }
    }

    public void MuteSwitcher()
    {
        if (coinSound != null || Button != null && audioSource !=null)
        {
            audioSource.mute = !audioSource.mute;
        }
        else if (Music != null && audioSource != null)
        {
            audioSource.mute = !audioSource.mute;
        }
    }
}
