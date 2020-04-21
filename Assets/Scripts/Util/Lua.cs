using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Ashel
{
    public class Lua
    {
        Script Script { get; set; }

        public Lua(string rawLuaCode)
        {
            Script = new Script();

            UserData.RegistrationPolicy = InteropRegistrationPolicy.Automatic;

            Script.DoString(rawLuaCode);
        }

        public DynValue Call(string key, object[] args)
        {
            DynValue result = Script.Call(Script.Globals[key], args);

            return result;
        }
    }

    public class LuaManager
    {
        private static bool Initialized { get; set; } = false;
        private  static string LuaDirectoryPath { get; set; }
        public static Dictionary<string, Lua> LuaFiles { get; private set; }

        private static void Initialize()
        {
            LuaFiles = new Dictionary<string, Lua>();
            LuaDirectoryPath = Path.Combine(Application.streamingAssetsPath, "Lua");

            SetUpLuaFiles();
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

                    LuaFiles.Add(fileName, new Lua(sr.ReadToEnd()));
                }
            }
        }

        public static DynValue Run(string fileName, string key, object[] args = null)
        {
            if(Initialized == false) { Initialize(); }

            return LuaFiles[fileName].Call(key, args);
        }
    }
}