using structs;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class ContentLoader : MonoBehaviour
{
    string jsonpath;
    string BundlePath;
    uint bundlever;
    public string remotejsonPath;
    AssetBundle bundle;
    GameObject Coin;
    private void Awake()
    {
        SceneSwitcher.SceneSwitch.AddListener(BundleCleaner);
        StartCoroutine(LoadBundle());
    }

    private void BundleCleaner()
    {
        bundle.UnloadAsync(true);
    }

    IEnumerator LoadBundle()
    {
        while (!Caching.ready)
            yield return null;

        UnityWebRequest wwww = UnityWebRequest.Get(remotejsonPath);
        yield return wwww.SendWebRequest();

        if (wwww.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wwww.error);
        }
        else
        {
            BundleData bundleData = new BundleData();
            string json = wwww.downloadHandler.text;
            bundleData = JsonUtility.FromJson<BundleData>(json);
            bundlever = bundleData.version;
            BundlePath = bundleData.path;
        }

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(BundlePath, bundlever, 0))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                yield break;
            }
            else
            {               
                bundle = DownloadHandlerAssetBundle.GetContent(www);
            }
          
            string SceneElements = "SceneElements.prefab";
            var sceneObjsAsset = bundle.LoadAssetAsync(SceneElements, typeof(GameObject));
            yield return bundle;
            GameObject loadElements = sceneObjsAsset.asset as GameObject;
            if (SceneManager.sceneCount > 1)
            {
                SceneManager.UnloadSceneAsync(0);
            }
            Instantiate(loadElements);
            string coinPref = "Coin.prefab";
            var coinAsset = bundle.LoadAssetAsync(coinPref, typeof(GameObject));
            Coin = coinAsset.asset as GameObject;

            StartCoroutine(CoinReplicator());
        }
    }

    private IEnumerator CoinReplicator()
    {
        while (true)
        {
            Vector3 spawnPoint;
            spawnPoint.x = Random.Range(-50, 50);
            spawnPoint.z = Random.Range(0, 100);
            spawnPoint.y = 30;
            GameObject newCoin = Instantiate(Coin);
            newCoin.transform.position = spawnPoint;
            newCoin.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

}
