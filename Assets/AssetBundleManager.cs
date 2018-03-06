using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetBundleManager  
{
    static private Dictionary<string, AssetBundleRef> dictAssetBundleRefs;
    static private bool isLoading = false;
    static AssetBundleManager()
    {
        dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();
    }

    private class AssetBundleRef {
        public AssetBundle assetBundle = null;
        public int version;
        public string url;
        public AssetBundleRef(string strUrl,int version)
        {
            url = strUrl;
            this.version = version;
        }
    }

    public static AssetBundle GetAssetBundle(string url,int version)
    {
        string keyName = url + version.ToString();
        AssetBundleRef abRef;
        if (dictAssetBundleRefs.TryGetValue(keyName,out abRef))
        {
            return abRef.assetBundle;
        }
        else
        {
            return null;
        }
    }

    public static IEnumerator DownloadAssetBundle(string url,int version)
    {
        string keyName = url + version.ToString();
        if (dictAssetBundleRefs.ContainsKey(keyName)||isLoading)
        {
            yield return null;
        }
        else
        {
            isLoading = true;
            AssetBundleRef abRef = new AssetBundleRef(url, version);
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(url);
            while (!request .isDone )
            {
                yield return null;
            }
            abRef.assetBundle = request.assetBundle;
            dictAssetBundleRefs.Add(keyName, abRef);
            isLoading = false;

        }

    }
}
