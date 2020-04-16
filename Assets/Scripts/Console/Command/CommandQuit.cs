using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ashel
{
    public class CommandQuit : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandQuit()
        {
            Name = "Quit";
            Command = "quit";
            Description = "Quit Applcationq";
            Help = "";

            AddCommandToConsole();
        }

        public override bool RunCommand(string[] inputs)
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
            return true;
        }

        public static CommandQuit CreateCommand()
        {
            return new CommandQuit();
        }
    }
}