using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static char[,] map;
    static int playerX = 1;
    static int playerY = 1;
    static int playerHealth = 100;

    static void Main()
    {
        LoadMap("map.txt");
        DrawMap();

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey();

            int newX = playerX;
            int newY = playerY;

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    newY = playerY - 1;
                    break;
                case ConsoleKey.DownArrow:
                    newY = playerY + 1;
                    break;
                case ConsoleKey.LeftArrow:
                    newX = playerX - 1;
                    break;
                case ConsoleKey.RightArrow:
                    newX = playerX + 1;
                    break;
            }
            if (map[newY, newX] == '╝' || map[newY, newX] == '╬' ||
                map[newY, newX] == '║' || map[newY, newX] == '═' ||
                map[newY, newX] == '╣' || map[newY, newX] == '╠' ||
                map[newY, newX] == '╔' || map[newY, newX] == '╚' ||
                map[newY, newX] == '╗' || map[newY, newX] == '╩' || 
                map[newY, newX] == '╦')
            {
                playerHealth -= 10; // Уменьшаем здоровье при столкновении со стеной
                Console.WriteLine("Вы ударились о стену и потеряли 10 HP!");
                DrawHealthbar();
                if (playerHealth <= 0)
                {
                    Console.WriteLine("Вы проиграли!");
                    Console.ReadLine();
                    break;
                }
            }

            else if (map[newY, newX] == 'E') // Если игрок достиг выхода из лабиринта
            {
                Console.WriteLine("Поздравляю! Вы успешно прошли лабиринт!");
                Console.ReadLine();
                break; // Выход из цикла игры
            }
            else
            {
                playerX = newX;
                playerY = newY;
                Console.Clear();
                DrawMap();

                // Взаимодействие с врагами и другими элементами

                DrawHealthbar();

            }
        }
    }

    static void LoadMap(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        int rows = lines.Length;
        int cols = lines[0].Length;

        map = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = lines[i][j];
            }
        }
    }

    static void DrawMap()
    {
        
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i == playerY && j == playerX)
                    {
                        Console.Write("P"); // Отображение игрока
                    }
                    else
                    {
                        char currentSymbol = map[i, j];

                        // Отображение различных символов стен
                        if (currentSymbol == '╝' || currentSymbol == '╬' || currentSymbol == '║' || currentSymbol == '═' || currentSymbol == '╣' || currentSymbol == '╠' || currentSymbol == '╚' ||
                            currentSymbol == '╔' || currentSymbol == '╗' || currentSymbol == '╩' || currentSymbol == '╦' || currentSymbol == 'E')
                        {
                            Console.Write(currentSymbol);
                        }
                        else 
                        {
                            Console.Write(" "); // Отображение пустого пространства
                        }
                    }
                }
                Console.WriteLine();
            }

    }

    static void DrawHealthbar()
    {
        int filledSymbols = playerHealth / 10;
        int emptySymbols = 10 - filledSymbols;

        Console.Write("[");
        Console.Write(new string('#', filledSymbols));
        Console.Write(new string('_', emptySymbols));
        Console.Write($"] {playerHealth}% \n");
    }
}

