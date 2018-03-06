using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Creater : MonoBehaviour 
{
    private AssetBundleManifest manifest;
    private AssetBundle manifestBundle;
    private void Update() 
	{
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(CreateOBJByName("CharacterRobotBoy","test.unity3d"));
        }

    }
	
	private IEnumerator CreateOBJByName(string objName,string bundleName) 
	{
        yield return StartCoroutine(AssetBundleManager.DownloadAssetBundle(Path.Combine(AppConst.DataPath, "Streamingassets"), 0));
        manifestBundle = AssetBundleManager.GetAssetBundle(Path.Combine(AppConst.DataPath, "Streamingassets"), 0);

        //AssetBundle abm = AssetBundle.LoadFromFile(AppConst.DataPath + "/StreamingAssets");
        manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
        string[] depends = manifest.GetAllDependencies(bundleName);

        AssetBundle[] dependsAssetBundle = new AssetBundle[depends.Length];

        for (int i = 0; i < depends.Length; i++)
        {
            //dependsAssetBundle[i]=AssetBundle.LoadFromFile(AppConst.DataPath + "/"+ depends[i]);
            yield return StartCoroutine(AssetBundleManager.DownloadAssetBundle(AppConst.DataPath+depends[i], 0));
            dependsAssetBundle[i] = AssetBundleManager.GetAssetBundle(AppConst.DataPath + depends[i], 0);
        }

        yield return StartCoroutine(AssetBundleManager.DownloadAssetBundle(AppConst.DataPath + bundleName, 0));
        AssetBundle ab = AssetBundleManager.GetAssetBundle(AppConst.DataPath + bundleName, 0);
        //AssetBundle ab = AssetBundle.LoadFromFile(AppConst.DataPath + "/test.unity3d");
        if (ab)
        {
            GameObject obj = ab.LoadAsset<GameObject>("CharacterRobotBoy");
            Instantiate(obj);
        }
    }

    //private IEnumerator CreateOBJByName2()
    //{
    //    WWW www = new WWW("http://192.168.11.1:8080/test.unity3d");
    //    yield return www;
    //    AssetBundle ab = www.assetBundle;
    //    GameObject obj = ab.LoadAsset<GameObject>("CharacterRobotBoy");
    //    Instantiate(obj);

    //}
}
