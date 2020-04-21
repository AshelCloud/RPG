using MoonSharp.Interpreter;
using UnityEngine;

namespace Ashel
{
    public partial class Player : MonoBehaviour
    {
        [System.NonSerialized]
        public Item EquipHead = null;
        [System.NonSerialized]
        public Item EquipWeapon = null;

        private void UpdateDataBase()
        {
            if (EquipHead != null)
            {
                Head.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + EquipHead.TextureName);
            }

            if (EquipWeapon != null)
            {
                Weapon.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + EquipWeapon.TextureName);
            }
        }

        public void AddItem(Item item)
        {
            switch(item.Part)
            {
                case ItemPart.Head:
                    if(EquipHead == null)
                    {
                        EquipItem(item);
                        return;
                    }
                break;

                case ItemPart.Weapon:
                    if(EquipWeapon == null)
                    {
                        EquipItem(item);
                        return;
                    }
                break;
            }

            Inventory.Instance.AddItem(item);
        }

        public void EquipItem(Item item)
        {
            Debug.Log(item.Name);

            switch(item.Part)
            {
                case ItemPart.Head:
                    EquipHead = item;
                break;

                case ItemPart.Weapon:
                    EquipWeapon = item;
                break;
            }

            Inventory.Instance.EquipItem(item);
        }
    }
}