using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Console
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        protected void AddCommandToConsole()
        {
            DeveloperConsole.Instance.AddMessageToConsole("Command" + Name + " Added.");
            DeveloperConsole.AddCommandToConsole(this);
        }

        public abstract bool RunCommand(string[] inputs);
    }

    public class DeveloperConsole : MonoBehaviour
    {
        public static DeveloperConsole Instance { get; private set; }
        public static Dictionary<string, ConsoleCommand> Commands { get; private set; }

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

            Commands = new Dictionary<string, ConsoleCommand>();

            CreateCommands();
        }

        private void Update()
        {   
            //Console On/Off
            if(Input.GetKeyDown(KeyCode.BackQuote))
            {
                consoleCanvas.gameObject.SetActive( !consoleCanvas.gameObject.activeSelf );
                IsEnable = consoleCanvas.gameObject.activeSelf;

                if(consoleCanvas.gameObject.activeSelf == true)
                {
                    inputField.ActivateInputField();
                }
                else
                {
                    inputField.text = "";
                }
            }

            if(consoleCanvas.gameObject.activeSelf == false)
            {
                return;
            }

            //Send Command
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if( !string.IsNullOrEmpty(inputField.text))
                {
                    AddMessageToConsole(inputField.text);
                    ParseInput(inputField.text);

                    inputField.text = "";

                    inputField.ActivateInputField();
                }
            }
        }

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

        private void CreateCommands()
        {
            CommandGive.CreateCommand();
            CommandQuit.CreateCommand();
        }

        public static void AddCommandToConsole(ConsoleCommand _command)
        {
            Commands.Add(_command.Command, _command);
        }

        private void ParseInput(string input)
        {
            string[] inputs = input.Split(null);

            if(Commands.ContainsKey(inputs[0]))
            {
                bool result = Commands[inputs[0]].RunCommand(inputs);

                if(result == false)
                {
                    Debug.Log("Wrong Command!" + "<color=#ff0000>" + " Usage: " + Commands[inputs[0]].Help + "</color>");
                }
            }
            else
            {
                Debug.Log("Not Allowed Command!");
            }
        }
    }
}
