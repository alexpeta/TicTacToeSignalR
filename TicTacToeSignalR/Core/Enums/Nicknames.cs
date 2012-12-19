using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeSignalR.Core.Enums
{
    public static class NicknamesHelper
    {
        private static List<string> Nicks = new List<string>()
        {
            "Captain X",
            "Superman",
            "Joe",
            "Ariel",
            "Wolverine",
            "Professor X",
            "Clark Kent",
            "Green Lantern",
            "Fudge",
            "Uncle Scrooge",
            "Goofy",
            "Veronica Lodge",
            "Betty Cooper",
            "Thing",
            "Cousin It",
            "Mr. Fantastic",
            "Dick Grayson",
            "Wonder Woman",
            "Jughead Jones",
            "Invisible Woman",
            "Colossus",
            "Jean Grey",
            "Iceman",
            "Nightcrawler",
            "Lois Lane",
            "Daisy Duck",
            "Emma Frost",
            "Gyro Gearloose",
            "Nick Fury",
            "Magneto",
            "Lex Luthor",
            "Hawkeye",
            "Doctor Strange",
            "Hal Jordan",
            "Scarlet Witch",
            "Hank Pym",
            "Scarlet Witch",
            "Batman",
            "Alfred",
            "James Gordon",
            "Cannonball",
            "Green Arrow",
            "Martian Manhunter",
            "Mary Jane",
            "Psylocke",
            "Jimmy Olsen",
            "Doctor Doom",
            "J. Jonah Jameson",
            "Agent Smith",
            "Richie Rich",
            "Supergirl",
            "Hawkman",
            "Black Pete",
            "Black Widow",
            "Gladstone Gander"
        };

        public static string GetRandomNick()
        {
            Random r = new Random();
            return Nicks[r.Next(0,Nicks.Count-1)];
        }
    }
}