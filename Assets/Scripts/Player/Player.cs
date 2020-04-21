using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ashel
{
    [MoonSharpUserData]
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

        //private void Start()
        //{
        //    string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Lua");
        //    filePath = System.IO.Path.Combine(filePath, "Test.lua");

        //    string text = System.IO.File.ReadAllText(filePath);

        //    var lua = new Lua(text); 

        //    Debug.Log(lua.Call("TestFuc", new object[] { "Cool! It's Working" } ).String);
        //}

        private void Update()
        {
            UpdateDataBase();
            Movement();
        }
    }
}