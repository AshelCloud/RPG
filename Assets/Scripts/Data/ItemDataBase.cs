using System.Collections.Generic;

namespace Ashel
{
    public enum ItemPart
    {
        Head,
        Weapon,
        None
    }
    public abstract class Item
    {
        public abstract int Code { get; protected set; }
        public abstract string Name { get; protected set; }
        public abstract string TextureName { get; protected set; }
        public abstract ItemPart Part { get; protected set; }

    }
    public class SwordManHelmet : Item
    {
        public override int Code { get; protected set; } = 0;
        public override string Name { get; protected set; } = "검남자의 헬멧";
        public override string TextureName { get; protected set; } = "Hat-Helmet";
        public override ItemPart Part { get; protected set; } = ItemPart.Head;
    }

    public class SwordManSword : Item
    {
        public override int Code { get; protected set; } = 1;
        public override string Name { get; protected set; } = "검남자의 검";
        public override string TextureName { get; protected set; } = "Weapon-Sword";
        public override ItemPart Part { get; protected set; } = ItemPart.Weapon;
    }

    //TODO: Json으로 파싱해서 데이터 받기
    public class ItemDataBase
    {
        static private bool Initialized { get; set; } = false;
        static List<Item> database = new List<Item>();

        private static bool Initialize()
        {
            database.Add(new SwordManHelmet());
            database.Add(new SwordManSword());

            Initialized = true;

            return true;
        }

        public static Item GetItem(int code)
        {
            if(!Initialized) { Initialize(); }

            foreach(var data in database)
            {
                if(data.Code == code)
                {
                    return data;
                }
            }

            return null;
        }

        public static Item GetItem(string name)
        {
            if (!Initialized) { Initialize(); }

            foreach (var data in database)
            {
                if(data.Name == name)
                {
                    return data;
                }
            }

            return null;
        }
    }
}