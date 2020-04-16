using Ashel;
using UnityEngine;
using UnityEngine.UI;

namespace Ashel
{
    public class ItemSlot : MonoBehaviour
    {
        public ItemPart part;
        public Item Item { get; set; }

        Vector2 Ratio { get; set; }
        private void Awake()
        {
            Ratio = GetComponent<RectTransform>().sizeDelta;
        }

        private void Start()
        {
            SetRatioItemImage();
        }

        public void AddItem(Item item)
        {
            Item = item;

            var image = transform.GetChild(0).GetComponent<Image>();

            image.sprite = Resources.Load<Sprite>("Sprites/" + Item.TextureName);
            image.SetNativeSize();

            SetRatioItemImage();
        }

        //A : B 는 C : X 일때 X = (B * C) / A
        private void SetRatioItemImage()
        {
            var rect = transform.GetChild(0).GetComponent<RectTransform>();

            var imageSize = rect.sizeDelta;

            if(imageSize.x > Ratio.x)
            {
                float tmp = imageSize.x - Ratio.x;

                imageSize.x -= tmp;

                imageSize.y = (imageSize.y * imageSize.x) / ( imageSize.x + tmp );
            }

            if (imageSize.y > Ratio.y)
            {
                float tmp = imageSize.y - Ratio.y;

                imageSize.y -= tmp;

                imageSize.x = (imageSize.x * imageSize.y) / (imageSize.y + tmp);
            }

            rect.sizeDelta = imageSize;
        }
    }
}