using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GroundChecker : MonoBehaviour
    {
        private PlayerController root { get; set; }

        public LayerMask ground;
        public float radius;

        private void Awake()
        {
            root = transform.root.GetComponent<PlayerController>();
        }

        private void Update()
        {
            root.Grounded = Physics2D.OverlapCircle(transform.position, radius, ground);
        }
    }
}