using Ashel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ashel
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public partial class Player : MonoBehaviour
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Anim { get; private set; }

        public bool Grounded { get; set; }

        private void InitializeController()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Anim = Model.GetComponent<Animator>();
        }

        private void Movement()
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

            //Jump
            if(Input.GetKeyDown(KeyCode.Space) && Grounded)
            {
                Rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            //Animation
            if(!Grounded)
            {
                Anim.Play("Jump");
            }

            if (direction.x != 0 && Grounded)
            {
                Anim.Play("Run");
            }
            else if(Grounded)
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