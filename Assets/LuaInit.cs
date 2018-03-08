using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

[Hotfix]
public class LuaInit : MonoBehaviour 
{
    private static LuaEnv luaEnv;

	private void Start() 
	{
        luaEnv = new LuaEnv();

    }
	
	private void Update () 
	{
        //print("print from c#");

        if (Input.GetKeyDown(KeyCode.I))
        {
            InitAllLuaScripts();
        }
	}

    private void InitAllLuaScripts()
    {
        string luaScriptsPath = Path.Combine(AppConst.DataPath, "LuaScripts");

        string[] fileNames = Directory.GetFiles(luaScriptsPath);

        for (int i = 0; i < fileNames.Length; i++)
        {
            byte[] bytes = File.ReadAllBytes(fileNames[i]);
            luaEnv.DoString(System.Text.Encoding.UTF8.GetString(bytes));
        }
        print("All lua injected");
    }
}
