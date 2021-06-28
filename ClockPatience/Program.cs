using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockPatience
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, List<Card>> deck = new Dictionary<char, List<Card>>();
            string line;
            while ((line = Console.ReadLine().Trim()) != "#")
            {
                string[] arr = line.Trim().Split(' ');
                for (int i = 0; i < 13; i++)
                {
                    string item = arr[i];
                    char position = i switch
                    {
                        0 => 'A',
                        1 => '2',
                        2 => '3',
                        3 => '4',
                        4 => '5',
                        5 => '6',
                        6 => '7',
                        7 => '8',
                        8 => '9',
                        9 => 'T',
                        10 => 'J',
                        11 => 'Q',
                        12 => 'K'
                    };
                    deck.TryGetValue(position, out List<Card> currList);
                    if (currList != null)
                        currList.Add(new Card(item));
                    else
                    {
                        currList = new List<Card>();
                        currList.Add(new Card(item));
                        deck.Add(position, currList);
                    }
                }
            }

            int openedCardCount = 1;
            var currPile = deck['K'];
            Card currCard;

            do
            {
                currCard = currPile.Last();
                if (!currCard.open)
                {
                    currCard.open = true;
                    openedCardCount++;
                }

                currPile.Remove(currCard);
                deck[currCard.name[0]].Insert(0, currCard);
                currPile = deck[currCard.name[0]];

                if (currPile.Count(c => c.name[0] == 'K') == 4)
                    break;
            }
            while (currPile.Any(c => !c.open));

            Console.WriteLine($"{openedCardCount},{currCard.name}");
            Console.ReadKey();
        }
    }

    public class Card
    {
        public string name;
        public bool open = false;

        public Card(string name)
        {
            this.name = name;
        }
    }
}
