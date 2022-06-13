using structs;
using UnityEditor;
using UnityEngine;
using System.IO;

public class BundleCreator
{
    //Create item in menu

    [MenuItem("Assets/ Build Bundles")]

    //Method for building Asset Bundles


    static void BuildBundles()
    {
        //Write verson of new bundle, and it's path to json.

        string jsonpath = "Assets/AssetBundles/bundledata.json";
        if (!File.Exists(jsonpath))
        {
            BundleData newbandledate = new BundleData();
            newbandledate.path = "";
            newbandledate.version = 0;
            string newjson = JsonUtility.ToJson(newbandledate, prettyPrint: true);
            File.WriteAllText(jsonpath, newjson);
        }

        BundleData bundleData = new BundleData();
        string json = File.ReadAllText(jsonpath);
        bundleData = JsonUtility.FromJson<BundleData>(json);
        uint version = bundleData.version;
        version = version+1;
        bundleData.version = version;
        bundleData.path = "https://firebasestorage.googleapis.com/v0/b/testprojholyw.appspot.com/o/projcontent?alt=media&token=acf4f3e5-8783-43f1-94fa-57e5522725f6";
        string tojson = JsonUtility.ToJson(bundleData, prettyPrint: true);
        File.WriteAllText(jsonpath, tojson);

        //Create bundle

        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
        
}
