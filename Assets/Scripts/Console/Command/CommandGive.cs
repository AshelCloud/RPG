using UnityEngine;
using Ashel;

namespace Ashel
{
    public class CommandGive : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandGive()
        {
            Name = "Give";
            Command = "give";
            Description = "Give item for user";
            Help = "give [itemCode]";

            AddCommandToConsole();
        }

        public override bool RunCommand(string[] inputs)
        {
            if(inputs.Length > 2 || inputs.Length < 2)
            {
                return false;
            }

            if(!int.TryParse(inputs[1], out int code))
            {
                return false;
            }

            //Give Item
            var item = ItemDataBase.GetItem(code);
            if(item == null)
            {
                Debug.LogError("There is no item with code.");
                return false;
            }

            GameObject.Find("Player").GetComponent<Ashel.Player>().AddItem(item);

            DeveloperConsole.Instance.AddMessageToConsole(item.Name + "이(가) 지급되었습니다.");

            return true;
        }

        public static CommandGive CreateCommand()
        {
            return new CommandGive();
        }
    }
}
