using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class Loader : MonoBehaviour 
{

	private void Start() 
	{
        StartCoroutine(OnUpdataResource());
	}
	
	IEnumerator OnUpdataResource() 
	{
        print("OnUpdataResource");
        string webListUrl = AppConst.WebUrl + "files.txt";

        UnityWebRequest request = UnityWebRequest.Get(webListUrl);
        yield return request.SendWebRequest();
        if (!Directory.Exists(AppConst.DataPath))
        {
            Directory.CreateDirectory(AppConst.DataPath);
        }
        
        File.WriteAllBytes(AppConst.DataPath + "files.txt", request.downloadHandler.data);

        string filesText = request.downloadHandler.text;
        string[] files = filesText.Split('\n');
        for (int i = 0; i < files.Length; i++)
        {
            if (string.IsNullOrEmpty(files[i])) continue;
            string localfile = (AppConst.DataPath + files[i]).Trim();
            string path = Path.GetDirectoryName(localfile);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }
            string webPath = AppConst.WebUrl + files[i];
            yield return StartCoroutine(Load(webPath, localfile));
        }
        print("all done");

        request.Dispose();

    }

    private IEnumerator Load(string webPath,string localPath)
    {
        UnityWebRequest request = UnityWebRequest.Get(webPath);
        yield return request.SendWebRequest();
        DownloadHandler handler = request.downloadHandler;
        File.WriteAllBytes(localPath, handler.data);
        print("done");
    }
}
