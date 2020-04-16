using UnityEngine;

namespace Ashel
{
    public class GroundChecker : MonoBehaviour
    {
        private Player root { get; set; }

        public LayerMask ground;
        public float radius;

        private void Awake()
        {
            root = transform.root.GetComponent<Player>();
        }

        private void Update()
        {
            root.Grounded = Physics2D.OverlapCircle(transform.position, radius, ground);
        }
    }
}