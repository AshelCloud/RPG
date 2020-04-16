﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ashel
{
    public partial class Player : MonoBehaviour
    {
        private Transform Model { get; set; }
        private Transform Body { get; set; }
        private Transform Head { get; set; }
        private Transform Weapon { get; set; }

        private void Awake()
        {
            Model = transform.Find("Model");
            Body = Model.Find("Body");
            Head = Body.Find("Head");
            Weapon = Body.Find("Weapon");

            InitializeController();
        }

        private void Update()
        {
            UpdateDataBase();
            Movement();
        }
    }
}