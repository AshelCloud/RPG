using Console;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Setting")]
        public float moveSpeed = 1f;
        public float jumpForce = 1f;
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Anim { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Anim = transform.Find("Model").GetComponent<Animator>();
        }

        private void Update()
        {
            //Movement
            if(DeveloperConsole.IsEnable) { return; }

            Vector3 direction = Vector3.zero;

            if(Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;

                if(!Input.GetKey(KeyCode.D)) { Flip(true); }
            }

            if(Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;

                if (!Input.GetKey(KeyCode.A)) { Flip(false); }
            }

            if(direction.x != 0)
            {
                Anim.Play("Run");
            }
            else
            {
                Anim.Play("Idle");
            }

            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }

        private void Flip(bool bLeft)
        {
            transform.localScale = bLeft == true ? Vector3.one : new Vector3(-1, 1, 1);
        }
    }
}