using MoonSharp.Interpreter;
using System.Collections.Generic;

namespace Ashel
{
    public enum ItemPart
    {
        Head,
        Weapon,
        None
    }

    [MoonSharpUserData]
    [System.Serializable]
    public class Item
    {
        public int Code;
        public string Name;
        public string TextureName;
        public ItemPart Part;
    }

    //TODO: Json으로 파싱해서 데이터 받기
    [MoonSharpUserData]
    public class ItemDataBase
    {
        static private bool Initialized { get; set; } = false;

        static private Dictionary<int, Item> database = new Dictionary<int, Item>();

        private static bool Initialize()
        {
            database = JsonManager.LoadJson<Serialization<int, Item>>(UnityEngine.Application.streamingAssetsPath + "/Json", "ItemData").ToDictionary();

            Initialized = true;

            return true;
        }

        public static Item GetItem(int code)
        {
            if(!Initialized) { Initialize(); }

            if(database.ContainsKey(code))
            {
                return database[code];
            }

            return null;
        }

        public static Item GetItem(string name)
        {
            if (!Initialized) { Initialize(); }

            foreach (var data in database)
            {
                if(data.Value.Name == name)
                {
                    return data.Value;
                }
            }

            return null;
        }
    }
}