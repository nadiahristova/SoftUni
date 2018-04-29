namespace TheSlum.GameEngine
{
    using System;
    using System.Linq;

    using Enums;
    using Entities;
    using Entities.Weapons;
    using Entities.Boosts;
    using Entities.Parents;    

    class FullEngine : Engine
    {
        protected override void ExecuteCommand(string[] inputParams)
        {
            switch (inputParams[0])
            {
                case "status":
                    PrintCharactersStatus(characterList);
                    break;
                case "create":
                    this.CreateCharacter(inputParams);
                    break;
                case "add":
                    this.AddItem(inputParams);
                    break;
                default:
                    base.ExecuteCommand(inputParams);
                    break;
            }
        }

        protected override void CreateCharacter(string[] inputParams)
        {
            Character character = null;
            Team team;

            switch (inputParams[5].ToLower())
            {
                case "red":
                    team = Team.Red;
                    break;
                case "blue":
                    team = Team.Blue;
                    break;
                default:
                    throw new ArgumentException("Unknown team.");
            }

            switch (inputParams[1].ToLower())
            {
                case "warrior":
                    character = new Warrior(
                        id: inputParams[2],
                        x: int.Parse(inputParams[3]),
                        y: int.Parse(inputParams[4]),
                        team: team);
                    break;
                case "healer":
                    character = new Healer(
                        id: inputParams[2],
                        x: int.Parse(inputParams[3]),
                        y: int.Parse(inputParams[4]),
                        team: team);
                    break;
                case "mage":
                    character = new Mage(
                        id: inputParams[2],
                        x: int.Parse(inputParams[3]),
                        y: int.Parse(inputParams[4]),
                        team: team);
                    break;
                default:
                    throw new ArgumentException("Wrong character.");
            }

            this.characterList.Add(character);
        }

    

        private new void AddItem(string[] inputParams)
        {
            Item item;
            switch (inputParams[2].ToLower())
            {
                case "axe":
                    item = new Axe(inputParams[3]);
                    break;
                case "shield":
                    item = new Shield(inputParams[3]);
                    break;
                case "injection":
                    item = new Injection(inputParams[3]);
                    break;
                case "pill":
                    item = new Pill(inputParams[3]);
                    break;
                default:
                    throw new ApplicationException("No such kind of item.");
            }

            string targetCharecterId = inputParams[1];

            var character = this.characterList.Where(ch => ch.IsAlive)
                .FirstOrDefault(c => c.Id == targetCharecterId);

            if (character == null)
                throw new ArgumentException("No character with id " + targetCharecterId);

            character.AddToInventory(item);
        }

    }

}
