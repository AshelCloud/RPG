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

        private void Update()
        {
            if(Item == null) { return; }

            var image = transform.GetChild(0);

            image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + Item.TextureName);
        }

        //A : B 는 C : X 일때 X = (B * C) / A
        private void SetRatioItemImage()
        {
            var rect = transform.GetChild(0).GetComponent<RectTransform>();

            var imageSize = rect.sizeDelta;

            //A = imageSize.x, B = imageSize.y, C = imageSize.x -= tmp
            if(imageSize.x > Ratio.x)
            {
                float tmp = imageSize.x - Ratio.x;

                imageSize.x -= tmp;

                imageSize.y = (imageSize.y * imageSize.x) / ( imageSize.x + tmp );
            }

            //A = imageSize.y, B = imageSize.x, C = imageSize.y -= tmp
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