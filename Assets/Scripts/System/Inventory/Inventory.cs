using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ashel
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance { get; private set; }
        [SerializeField] private Canvas inventoryCanvas = null;

        private ItemSlot[] itemSlots;

        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;

            itemSlots = GetComponentsInChildren<ItemSlot>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
            {
                inventoryCanvas.gameObject.SetActive( !inventoryCanvas.gameObject.activeSelf );
            }
        }

        public void AddItem(Item item)
        {
            foreach(var itemSlot in itemSlots)
            {
                if(itemSlot.part != ItemPart.None) { continue; }

                if(itemSlot.Item != null)
                {
                    itemSlot.Item = item;

                    return;
                }
            }
        }

        public void EquipItem(Item item)
        {
            foreach (var itemSlot in itemSlots)
            {
                if (itemSlot.Item == null && itemSlot.part == item.Part)
                {
                    itemSlot.Item = item;

                    return;
                }
            }
        }
    }
}