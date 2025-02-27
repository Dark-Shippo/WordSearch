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
            Random rnd = new Random();
            string wordsFilePath = "Words.txt";
            string[] AllWords = File.ReadAllLines(wordsFilePath);
            bool isSelecting = true;

            Console.WriteLine("Select your category." + "\n" + "Guild Wars 2" + "\n" + "Rimworld" + "\n" + "Sly Cooper" + "\n" + "Ratchet And Clank" + "\n" + "Dynasty Warriors" + "\n" + "World Of Warcraft" + "\n" + "League Of Legends" + "\n" + "Soulsborne" + "\n" + "Remnant From The Ashes" + "\n" + "Marvel");
            string? userInput = null;

            while (isSelecting)
            {
                userInput = Console.ReadLine()?.Trim().ToLower();
                bool isValidChoice = false;
                foreach (string category in AllWords)
                {
                    if (category.ToLower() == userInput.ToLower())
                    {
                        isValidChoice = true;
                        break;
                    }
                }
                if (!isValidChoice)
                {
                    Console.WriteLine("Invalid choice! Please select a valid category." + "\n");
                    continue;
                }
                else
                {
                    isSelecting = false;
                    break;
                }
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
            string[] randomLetter = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };


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

            WordPlacement(randomRow, randomColumn, chosenRow, WordSearchBoard, eightChosenWords);

            RandomLetterPlacement(randomLetter, WordSearchBoard);

            PrintBoard(WordSearchBoard);

            while (isPlaying)
            {
                Console.WriteLine("\n" + "Find these words!");
                foreach (string word in eightChosenWords)
                {
                    Console.WriteLine(word.ToString());
                }

                Console.WriteLine("Please type the Row number (0-19) and Column number (0-19) where you think the word is located.");
                Console.WriteLine("Example: 3 4 for Row 3, Column 4.");

                string[] input = Console.ReadLine()?.Split(' ');

                if (input == null || input.Length != 2)
                {
                    Console.WriteLine("Invalid input! Please provide two numbers separated by a space.");
                    continue;
                }

                bool isValidInputRow = int.TryParse(input[0], out int row); 
                bool isValidInputCol = int.TryParse(input[1], out int col);
                if (!isValidInputRow || row < 0 || row >= WordSearchBoard.Length || !isValidInputCol || col < 0 || col >= WordSearchBoard[0].Length)
                {
                    Console.WriteLine("Invalid coordinates! Please enter a valid row and column.");
                    continue;
                }

                string foundWord = CheckForWord(row, col, eightChosenWords, WordSearchBoard);
                if (!string.IsNullOrEmpty(foundWord))
                {
                    foundWords++;
                    MarkWordAsFound(row, col, foundWord, WordSearchBoard);
                    Console.WriteLine($"You found the word: {foundWord}");
                }
                else
                {
                    Console.WriteLine("No word found at that position. Try again.");
                }

                if (foundWords == 8)
                {
                    Console.WriteLine("You WIN! Press R to play again or Q to quit.");
                    string playAgain = Console.ReadLine()?.Trim().ToLower();
                    if (playAgain == "r")
                    {
                        Main(args); // Restart the game
                    }
                    else if (playAgain == "q")
                    {
                        isPlaying = false; // Exit the game
                    }
                }
            }
        }

        // outputs the word vertically in a forward direction
        static void VerticalPlacements(int randomRow, int randomColumn, string chosenRow, string chosenWord, string[] WordSearchBoard)
        {
            Random random = new Random();
            bool placed = false;
            while (!placed)
            {
                randomRow = random.Next(WordSearchBoard.Length);
                chosenRow = WordSearchBoard[randomRow];
                randomColumn = random.Next(chosenRow.Length);
                if ((randomRow + chosenWord.Length) <= WordSearchBoard.Length) // Ensure word fits
                {
                    bool canPlace = true;

                    for (int i = 0; i < chosenWord.Length; i++)
                    {
                        char currentChar = WordSearchBoard[randomRow + i][randomColumn];

                        // Ensure the space is either empty ('.') or already contains the correct letter
                        if (currentChar != '.' && currentChar != chosenWord[i])
                        {
                            canPlace = false;
                            break;
                        }
                    }

                    if (canPlace)
                    {
                        for (int i = 0; i < chosenWord.Length; i++)
                        {
                            WordSearchBoard[randomRow + i] = WordSearchBoard[randomRow + i].Substring(0, randomColumn) + chosenWord[i] + WordSearchBoard[randomRow + i].Substring(randomColumn + 1);
                        }
                        placed = true;
                    }
                }
            }
        }

        // outputs the word vertically in a reverse direction
        static void VerticalReversePlacements(int randomRow, int randomColumn, string chosenRow, string chosenWord, string[] WordSearchBoard)
        {
            Random random = new Random();
            bool placed = false;
            chosenWord = ReverseWord(chosenWord);
            while (!placed)
            {
                randomRow = random.Next(WordSearchBoard.Length);
                chosenRow = WordSearchBoard[randomRow];
                randomColumn = random.Next(chosenRow.Length);

                if ((randomRow - chosenWord.Length + 1) >= 0) // Ensure word fits from bottom to top
                {
                    bool canPlace = true;

                    for (int i = 0; i < chosenWord.Length; i++)
                    {
                        char currentChar = WordSearchBoard[randomRow - i][randomColumn];

                        // Ensure the space is either empty ('.') or already contains the correct letter
                        if (currentChar != '.' && currentChar != chosenWord[i])
                        {
                            canPlace = false;
                            break;
                        }
                    }

                    if (canPlace)
                    {
                        for (int i = 0; i < chosenWord.Length; i++)
                        {
                            
                            WordSearchBoard[randomRow - i] = WordSearchBoard[randomRow - i].Substring(0, randomColumn) + chosenWord[i] + WordSearchBoard[randomRow - i].Substring(randomColumn + 1);
                        }
                        placed = true;
                    }
                }
            }
        }

        // outputs the word horizontally in a forward direction
        static void HorizontalPlacements(int randomRow, int randomColumn, string chosenRow, string chosenWord, string[] WordSearchBoard, string[] eightChosenWords)
        {
            Random random = new Random();
            bool placed = false;
            while (!placed)
            {
                randomRow = random.Next(WordSearchBoard.Length);
                chosenRow = WordSearchBoard[randomRow];
                randomColumn = random.Next(chosenRow.Length);
                if ((randomColumn + chosenWord.Length) <= WordSearchBoard[randomRow].Length)
                {
                    bool canPlace = true; // Flag to check if placement is valid

                    for (int i = 0; i < chosenWord.Length; i++)
                    {
                        char currentChar = WordSearchBoard[randomRow][randomColumn + i];

                        // Check if space is either empty or matches the word's letter
                        if (currentChar != '.' && currentChar != chosenWord[i])
                        {
                            canPlace = false;
                            break;
                        }
                    }

                    if (canPlace)
                    {
                        // Now, we know it's safe to place the word without overwriting
                        WordSearchBoard[randomRow] = WordSearchBoard[randomRow].Substring(0, randomColumn) +
                                                     chosenWord +
                                                     WordSearchBoard[randomRow].Substring(randomColumn + chosenWord.Length);

                        placed = true;
                    }
                }
            }
        }

        // outputs the word horizontally in a reverse direction
        static void HorizontalReversePlacements(int randomRow, int randomColumn, string chosenRow, string chosenWord, string[] WordSearchBoard, string[] eightChosenWords)
        {
            Random random = new Random();
            bool placed = false;
            while (!placed)
            {
                randomRow = random.Next(WordSearchBoard.Length);
                chosenRow = WordSearchBoard[randomRow];
                randomColumn = random.Next(chosenRow.Length);
                if ((randomColumn + chosenWord.Length) <= WordSearchBoard[randomRow].Length)
                {
                    bool canPlace = true; // Flag to check if placement is valid

                    for (int i = 0; i < chosenWord.Length; i++)
                    {
                        char currentChar = WordSearchBoard[randomRow][randomColumn + i];

                        // Check if space is either empty or matches the word's letter
                        if (currentChar != '.' && currentChar != chosenWord[i])
                        {
                            canPlace = false;
                            break;
                        }
                    }
                    if (canPlace)
                    {
                        chosenWord = ReverseWord(chosenWord);
                        WordSearchBoard[randomRow] = WordSearchBoard[randomRow].Substring(0, randomColumn) + chosenWord + WordSearchBoard[randomRow].Substring(randomColumn + chosenWord.Length);
                        placed = true;
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

        static void WordPlacement(int randomRow, int randomColumn, string chosenRow, string[] WordSearchBoard, string[] eightChosenWords)
        {
            Random rnd = new Random();
            string chosenWord;

            for (int i = 0; i < 8; i++)
            {
                chosenWord = eightChosenWords[i];

                randomColumn = rnd.Next(chosenRow.Length);
                randomRow = rnd.Next(WordSearchBoard.Length);

                int VertOrHoriz = rnd.Next(0, 4);

                switch (VertOrHoriz)
                {
                    case 0:
                        HorizontalPlacements(randomRow, randomColumn, chosenRow, chosenWord, WordSearchBoard, eightChosenWords);
                        break;
                    case 1:
                        VerticalPlacements(randomRow, randomColumn, chosenRow, chosenWord, WordSearchBoard);
                        break;
                    case 2:
                        VerticalReversePlacements(randomRow, randomColumn, chosenRow, chosenWord, WordSearchBoard);
                        break;
                    case 3:
                        HorizontalReversePlacements(randomRow, randomColumn, chosenRow, chosenWord, WordSearchBoard, eightChosenWords);
                        break;
                }
            }
        }

        // replaces empty spaces with random letters
        static void RandomLetterPlacement(string[] randomLetter, string[] WordSearchBoard)
        {
            Random rnd = new Random();

            for (int i = 0; i < WordSearchBoard.Length; i++)
            {
                char[] rowChars = WordSearchBoard[i].ToCharArray();
                for (int j = 0; j < rowChars.Length; j++)
                {
                    if (rowChars[j] == '.')
                    {
                        rowChars[j] = randomLetter[rnd.Next(randomLetter.Length)][0];
                    }
                }
                WordSearchBoard[i] = new string(rowChars);
            }
        }

        static string CheckForWord(int row, int col, string[] words, string[] board)
        {
            // Logic to check if a word starts from the given coordinates (row, col)
            foreach (string word in words)
            {
                if (CheckWordAtCoordinates(word, row, col, board))
                {
                    return word;
                }
            }
            return string.Empty;
        }

        static bool CheckWordAtCoordinates(string word, int row, int col, string[] board)
        {
            // Check horizontally
            if (col + word.Length <= board[row].Length)
            {
                bool match = true;
                for (int i = 0; i < word.Length; i++)
                {
                    if (board[row][col + i] != word[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match) return true;
            }

            // Check vertically
            if (row + word.Length <= board.Length)
            {
                bool match = true;
                for (int i = 0; i < word.Length; i++)
                {
                    if (board[row + i][col] != word[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match) return true;
            }

            return false;
        }

        static void MarkWordAsFound(int row, int col, string word, string[] board)
        {
            // Mark the word on the board in red
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < word.Length; i++)
            {
                board[row] = board[row].Substring(0, col + i) + word[i] + board[row].Substring(col + i + 1);
            }

            Console.ResetColor();
            PrintBoard(board);
        }

        static void PrintBoard(string[] board)
        {
            foreach (string row in board)
            {
                Console.WriteLine(row);
            }
        }
    }
}
