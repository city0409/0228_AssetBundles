using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour 
{

	private void Start() 
	{
        StartCoroutine(CreateOBJByName2());

    }
	
	private void CreateOBJByName() 
	{
        AssetBundle abm = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/StreamingAssets");
        AssetBundleManifest manifest = (AssetBundleManifest)abm.LoadAsset("AssetBundleManifest");
        string[] depends = manifest.GetAllDependencies("test.unity3d");

        AssetBundle[] dependsAssetBundle = new AssetBundle[depends.Length];

        for (int i = 0; i < depends.Length; i++)
        {
            dependsAssetBundle[i]=AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/"+ depends[i]);
        }

        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/test.unity3d");
        GameObject obj = ab.LoadAsset<GameObject>("CharacterRobotBoy");
        Instantiate(obj);

    }

    private IEnumerator CreateOBJByName2()
    {
        WWW www = new WWW("http://192.168.11.1:8080/test.unity3d");
        yield return www;
        AssetBundle ab = www.assetBundle;
        GameObject obj = ab.LoadAsset<GameObject>("CharacterRobotBoy");
        Instantiate(obj);

    }
}
