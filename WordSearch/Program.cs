namespace WordSearch
{
    using System.IO;
    using System.Net.Sockets;
    using System.Reflection.Metadata.Ecma335;
    using System.Security.Cryptography.X509Certificates;
    using System.Text.RegularExpressions;
    using System.Threading;
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfWords = 15;

            string wordsFilePath = "Words.txt";
            string[] AllWords = File.ReadAllLines(wordsFilePath);

            Console.WriteLine("Select your category." + "\n" + "Guild Wars 2" + "\n" + "Rimworld" + "\n" + "Sly Cooper" + "\n" + "Ratchet And Clank" + "\n" + "Dynasty Warriors" + "\n" + "World Of Warcraft" + "\n" + "League Of Legends" + "\n" + "Soulsborne" + "\n" + "Remnant From The Ashes" + "\n" + "Marvel");
            bool isSelecting = true;
            string? userInput = Console.ReadLine()?.Trim().ToLower();
            while (isSelecting)
            {


                if (!Array.Exists(AllWords, choice => choice.Equals(userInput, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Invalid choice!." + "\n");
                    continue;
                }
                isSelecting = false;
            }

            string[][] categoriesAndWords = new string[10][];
            for (int i = 0; i < categoriesAndWords.Length; i++)
            {
                categoriesAndWords[i] = new string[numberOfWords + 1];
                for (int j = 0; j < categoriesAndWords[i].Length; j++)
                {
                    categoriesAndWords[i][j] = AllWords[i * (numberOfWords + 1) + j];
                }
            }

            string[] eightChosenWords = new string[8];
            Random rnd = new Random();
            for (int i = 0; i < categoriesAndWords.Length; i++)
            {
                if (categoriesAndWords[i][0].ToLower() == userInput)
                {
                    for (int j = 0; j < eightChosenWords.Length; j++)
                    {
                        int randomIndex = rnd.Next(1, categoriesAndWords[i].Length);
                        string potentialWord = categoriesAndWords[i][randomIndex];
                        if (eightChosenWords.Contains(potentialWord))
                        {
                            j--;
                            continue;
                        }
                        eightChosenWords[j] = potentialWord;
                    }
                    break;
                }
            }

            bool isPlaying = true;
            int foundWords = 0;
            string[] randomLetter = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            string chosenWord;
            int randomLetterIndex = rnd.Next(randomLetter.Length);

            string[] WordSearchBoard =
            {
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                    "....................",
                };

            int randomRow = rnd.Next(WordSearchBoard.Length);
            string chosenRow = WordSearchBoard[randomRow];
            int randomColumn = rnd.Next(chosenRow.Length);

            for (int i = 0; i < eightChosenWords.Length; i++)
            {
                chosenWord = eightChosenWords[i];

                int VertOrHoriz = rnd.Next(1, 2);

                if (VertOrHoriz == 1)
                {
                    HorizontalPlacements(randomRow, randomColumn, chosenRow, chosenWord, WordSearchBoard, eightChosenWords);
                }
                else if (VertOrHoriz == 2)
                {
                    VerticalPlacements(randomRow, randomColumn, chosenRow, chosenWord, WordSearchBoard);
                }
            }


            foreach (string row in WordSearchBoard)
            {
                Console.WriteLine(row.ToString());
            }

            while (isPlaying)
            {
                //for (int i = 0;)


                Console.WriteLine("\n" + "Find these words!");
                foreach (string word in eightChosenWords)
                {
                    Console.WriteLine(word.ToString());
                }

                Console.WriteLine("Please type the Row number with the correct letter.");
                int Row = Console.Read();
                Console.WriteLine("Please type the Collumn number with the correct letter.");
                int Col = Console.Read();

                if (foundWords == 8)
                {
                    Console.WriteLine("You WIN! Press R to play again or Q to quit.");
                }
            }

            static void VerticalPlacements(int randomRow, int randomColumn, string chosenRow, string chosenWord, string[] WordSearchBoard)
            {
                Random random = new Random();
                int vertChoice = random.Next(0, 1);
                // outputs the word vertically in a forward direction
                if (vertChoice == 0)
                {
                    if ((randomRow + chosenWord.Length) < WordSearchBoard.Length)
                    {
                        for (int i = 0; i < chosenWord.Length; i++)
                        {
                            if (chosenRow[randomColumn] == '.')
                            {
                                WordSearchBoard[randomRow + i] = WordSearchBoard[randomRow].Substring(0, randomColumn) + chosenWord.Substring(i, 1) + WordSearchBoard[randomRow].Substring(randomColumn + 1);
                            }
                        }
                    }
                    else if (!((randomRow + chosenWord.Length) < WordSearchBoard.Length))
                    {
                        randomRow = random.Next(WordSearchBoard.Length);
                        chosenRow = WordSearchBoard[randomRow];
                        randomColumn = random.Next(chosenRow.Length);
                        VerticalPlacements(randomRow, randomColumn, chosenRow, chosenWord, WordSearchBoard);
                    }
                }
                // outputs the word vertically in a reverse direction
                if (vertChoice == 1)
                {
                    if ((randomRow + chosenWord.Length) < WordSearchBoard.Length)
                    {
                        for (int i = 0; i < chosenWord.Length; i++)
                        {
                            if (chosenRow[randomColumn] == '.')
                            {
                                WordSearchBoard[randomRow + i] = WordSearchBoard[randomRow].Substring(0, randomColumn) + chosenWord.Substring(chosenWord.Length - 1 - i, 1) + WordSearchBoard[randomRow].Substring(randomColumn + 1);
                            }
                        }
                    }
                    else if (!((randomRow + chosenWord.Length) < WordSearchBoard.Length))
                    {
                        randomRow = random.Next(WordSearchBoard.Length);
                        chosenRow = WordSearchBoard[randomRow];
                        randomColumn = random.Next(chosenRow.Length);
                        VerticalPlacements(randomRow, randomColumn, chosenRow, chosenWord, WordSearchBoard);
                    }
                }
            }

            static void HorizontalPlacements(int randomRow, int randomColumn, string chosenRow, string chosenWord, string[] WordSearchBoard, string[] eightChosenWords)
            {
                Random random = new Random();
                int horizChoice = random.Next(0, 1);
                // outputs the word horizontally in a forward direction
                if (horizChoice == 0)
                {
                    if ((randomColumn + chosenWord.Length) < WordSearchBoard[randomRow].Length)
                    {
                        if (chosenRow[randomColumn] == '.')
                        {
                            WordSearchBoard[randomRow] = WordSearchBoard[randomRow].Substring(0, randomColumn) + chosenWord + WordSearchBoard[randomRow].Substring(randomColumn + chosenWord.Length);
                        }
                    }
                    else if (!((randomColumn + chosenWord.Length) < WordSearchBoard[randomRow].Length))
                    {
                        randomRow = random.Next(WordSearchBoard.Length);
                        chosenRow = WordSearchBoard[randomRow];
                        randomColumn = random.Next(chosenRow.Length);
                        HorizontalPlacements(randomRow, randomColumn, chosenRow, chosenWord, WordSearchBoard, eightChosenWords);
                    }
                }

                // outputs the word horizontally in a reverse direction
                if (horizChoice == 1)
                {
                    chosenWord = ReverseWord(chosenWord);
                    if ((randomColumn + chosenWord.Length) < WordSearchBoard[randomRow].Length)
                    {
                        if (chosenRow[randomColumn] == '.')
                        {
                            WordSearchBoard[randomRow] = WordSearchBoard[randomRow].Substring(0, randomColumn) + chosenWord + WordSearchBoard[randomRow].Substring(randomColumn + chosenWord.Length);
                        }
                    }
                    else if (!((randomColumn + chosenWord.Length) < WordSearchBoard[randomRow].Length))
                    {
                        randomRow = random.Next(WordSearchBoard.Length);
                        chosenRow = WordSearchBoard[randomRow];
                        randomColumn = random.Next(chosenRow.Length);
                        HorizontalPlacements(randomRow, randomColumn, chosenRow, chosenWord, WordSearchBoard, eightChosenWords);
                    }
                }
            }
        }

        // Converts the input word into a char array then using the array.reverse it flips the word and outputs the result.
        public static string ReverseWord(string word)
        {
            char[] reverseWord = word.ToCharArray();
            Array.Reverse(reverseWord);
            return new string(reverseWord);
        }

    }
}
