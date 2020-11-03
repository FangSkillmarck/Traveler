using System;
using System.Collections.Generic;

namespace Traveler
{
    public static class TravelParser
    {
        public static (int x, int y, char direction)[] Run(string input)
        {
            var result = new List<(int, int, char)>();
            string[] positionsList = input.Split("\n");

            var i = 0;
            while (true)
            {
                if (!positionsList[i].Contains("POS="))
                {
                    i++;
                }

                var tempList = positionsList[i].Split('='); 
            
                var newPosition = (int.Parse(tempList[1].Split(',')[0]), int.Parse(tempList[1].Split(',')[1]), tempList[1].Split(',')[2][0]);
                i += 1;

                var quitFlag = false;
              
                while (i < positionsList.Length)
                {
                    if (positionsList[i].StartsWith("POS="))
                    {
                        quitFlag = !quitFlag;
                        break;
                    }

                    foreach (var op in positionsList[i])
                    {
                        var d = op;
                        if (op == 'F')
                        {
                            switch (newPosition.Item3)
                            {
                                case 'N': newPosition.Item2 -= 1; break;
                                case 'S': newPosition.Item2 += 1; break;
                                case 'E': newPosition.Item1 += 1; break;
                                case 'W': newPosition.Item1 -= 1; break;
                            }
                        }
                        else if (op == 'B')
                        {
                            switch (newPosition.Item3)
                            {
                                case 'N': newPosition.Item2 += 1; break;
                                case 'S': newPosition.Item2 -= 1; break;
                                case 'E': newPosition.Item1 -= 1; break;
                                case 'W': newPosition.Item1 += 1; break;
                            }
                        }
                        else if (op == 'L' || op == 'R')
                        {
                            var newRotation = Rotate(newPosition.Item3, op);
                            newPosition.Item3 = newRotation;
                        }
                    }
                    i++;
                }
                 
                result.Add(newPosition);

                if (i >= positionsList.Length)
                {
                    break;
                }
            }
          
            return result.ToArray();
        }

        private static char Rotate(char facingDirection, char moveOrRotate)
        {
            switch (facingDirection)
            {
                case 'N':
                    {
                        switch (moveOrRotate)
                        {
                            case 'F': return 'N';
                            case 'B': return 'N';
                            case 'L': return 'W';
                            case 'R': return 'E';
                        }
                        break;
                    }
                case 'S':
                    {
                        switch (moveOrRotate)
                        {
                            case 'F': return 'S';
                            case 'B': return 'S';
                            case 'L': return 'E';
                            case 'R': return 'W';
                        }
                        break;
                    }
                case 'E':
                    {
                        switch (moveOrRotate)
                        {
                            case 'F': return 'E';
                            case 'B': return 'E';
                            case 'L': return 'N';
                            case 'R': return 'S'; 
                        }
                        break;
                    }
                case 'W':
                    {
                        switch (moveOrRotate)
                        {
                            case 'F': return 'W';
                            case 'B': return 'W';
                            case 'L': return 'S';
                            case 'R': return 'N';
                        }
                        break;
                    }
            }

            throw new InvalidOperationException("Could not rotate!");
        }
    }
}
