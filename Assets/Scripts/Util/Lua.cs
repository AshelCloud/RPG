using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Ashel
{
    public class LuaManager
    {
        private static bool Initialized { get; set; } = false;
        private  static string LuaDirectoryPath { get; set; }
        public static List<Script> LuaFiles;

        private static void Initialize()
        {
            LuaFiles = new List<Script>();

            LuaDirectoryPath = Path.Combine(Application.streamingAssetsPath, "Lua");

            SetUpLuaFiles();

            Initialized = true;
        }

        private static void SetUpLuaFiles()
        {
            var fileInfo = new DirectoryInfo(LuaDirectoryPath).GetFiles();

            foreach (var file in fileInfo)
            {
                if (file.Name.Contains("meta")) { continue; }

                using (StreamReader sr = file.OpenText())
                {
                    string fileName = file.Name.Split('.')[0];

                    Script script = new Script();

                    RegisterLua(script);
                    script.DoString(sr.ReadToEnd());
                    
                    LuaFiles.Add(script);
                }
            }
        }

        public static DynValue CallLuaFunction(string functionName, params object[] parameters)
        {
            if( Initialized == false ) { Initialize(); }

            foreach(var lua in LuaFiles)
            {
                if(lua.Globals[functionName] != null)
                {
                    return lua.Call(lua.Globals[functionName], parameters);
                }
            }
            
            return null;
        }

        private static void RegisterLua(Script script)
        {
            UserData.RegisterAssembly();

            if(script.Globals != null)
            {
                script.Globals["Console"] = new Console();
            }
        }
    }
}