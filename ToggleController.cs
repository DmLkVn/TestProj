using structs;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class ToggleController : MonoBehaviour
{
    public Toggle musictoggle;
    public Toggle sfxtoggle;
    GameData gameData;
    string jsonpath;
    string filename = "data.json";



    void Start()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        jsonpath = Path.Combine(Application.persistentDataPath, filename);
#else
        jsonpath = Path.Combine(Application.dataPath, filename);
#endif
        Deserialisation();
    }

    private void Update()
    {
        if ((musictoggle.isOn != gameData.musicmute) || (sfxtoggle.isOn != gameData.sfxmute))
        {
            Serialisation();
        }
    }

    private void Serialisation()
    {
        GameData gameDatanew = new GameData
        {
            musicmute = this.musictoggle.isOn,
            sfxmute = this.sfxtoggle.isOn,
        };
        string json = JsonUtility.ToJson(gameDatanew, prettyPrint: true);
        File.WriteAllText(jsonpath, contents: json);
    }

    private void Deserialisation()
    {
        if (File.Exists(jsonpath))
        {
            string json = File.ReadAllText(jsonpath);
            gameData = JsonUtility.FromJson<GameData>(json);
            musictoggle.isOn = gameData.musicmute;
            sfxtoggle.isOn = gameData.sfxmute;
        }
        else
        {
            Serialisation();
        }
    }
}
