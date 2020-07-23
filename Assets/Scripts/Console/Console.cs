using MoonSharp.Interpreter;
using UnityEngine;
using UnityEditor;

namespace Ashel
{
    //Lua를 위한 외부함수
    [MoonSharpUserData]
    public class Console
    {
        public Player Player
        {
            get
            {
                return GameObject.FindObjectOfType(typeof(Player)) as Player;
            }
        }

        public void Log(object data)
        {
            Debug.Log(data);
        }

        public void LogError(object data)
        {
            Debug.LogError(data);
        }

        public Item GetItem(int code)
        {
            return ItemDataBase.GetItem(code);
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}