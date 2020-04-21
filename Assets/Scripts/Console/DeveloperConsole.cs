using MoonSharp.Interpreter;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ashel
{
    public class DeveloperConsole : MonoBehaviour
    {
        public static DeveloperConsole Instance { get; private set; }

        [Header("UI Components")]
        [SerializeField] private Canvas consoleCanvas = null;
        [SerializeField] private InputField inputField = null;
        [SerializeField] private Text consoleText = null;

        public static bool IsEnable { get; private set;}

        private void Awake()
        {
            if(Instance != null)
            {
                return;
            }

            Instance = this;

            consoleCanvas.gameObject.SetActive(false);
            IsEnable = consoleCanvas.gameObject.activeSelf;

        }

        private void Update()
        {   
            //Console On/Off
            if(Input.GetKeyDown(KeyCode.BackQuote))
            {
                consoleCanvas.gameObject.SetActive( IsEnable = !IsEnable );

                if(IsEnable)
                {
                    inputField.ActivateInputField();
                }
                else
                {
                    inputField.text = "";
                }
            }

            if(IsEnable == false)
            {
                return;
            }

            //커맨드 전송
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if( !string.IsNullOrEmpty(inputField.text))
                {
                    AddMessageToConsole(inputField.text);
                    ParseInput(inputField.text);

                    //InputField 초기화
                    inputField.text = "";
                    inputField.ActivateInputField();
                }
            }
        }

        //로그 콘솔창에 띄우기 위한 이벤트 함수 저장
        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string logMessage, string stackTrace, LogType type)
        {
            string _message = "[" + type.ToString() + "] " + logMessage;
            AddMessageToConsole(_message);
        }

        public void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
        }

        private void ParseInput(string input)
        {
            string[] inputs = input.Split(null);

            string[] parameters = new string[inputs.Length - 1];
            for (int i = 0; i < inputs.Length - 1; i++)
            {
                parameters[i] = inputs[i + 1];
            }

            DynValue result = LuaManager.CallLuaFunction(inputs[0], parameters);

            if (result.Boolean == false)
            {
                //Debug.Log("Wrong Command!" + "<color=#ff0000>" + " Usage: " + Commands[inputs[0]].Help + "</color>");
            }
            else
            {
                //Debug.Log("Not Allowed Command!");
            }
        }
    }
}
