using Escape_from_Labyrinth;
using System;
using System.Collections.Generic;
using System.Text;
public class EscapeFromLabyrinth
{
    static int width = 9;
    static int height = 7;
    const char VisitedCell = 's';
    static Point FindStartPosition()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++ )
            {
                if(labyrinth[y,x] == VisitedCell)
                {
                    Point beginningPoint = new Point();
                    beginningPoint.X = x;
                    beginningPoint.Y = y;
                    return beginningPoint;
                }
            }
        }

        return null;
    }

    static char[,] labyrinth = 
    {
        {'*', '*', '*', '*', '*', '*', '*', '*', '*'},
        {'*', '-', '-', '-', '-', '*', '*', '-', '-'},
        {'*', '*', '-', '*', '-', '-', '-', '-', '*'},
        {'*', '-', '-', '*', '-', '*', '-', '*', '*'},
        {'*', 's', '*', '-', '-', '*', '-', '*', '*'},
        {'*', '*', '-', '-', '-', '-', '-', '-', '*'},
        {'*', '*', '*', '*', '*', '*', '*', '-', '*'}
    };
    static string FindShortestWayToExit()
    {
        var queue = new Queue<Point>();
        Point startPosition = FindStartPosition();
        if (startPosition == null)
        {
            return null;
        }
        queue.Enqueue(startPosition);
        while(queue.Count > 0)
        {
            var currNode = queue.Dequeue();
            if(IsEXit(currNode))
            {
                return TracePathBack(currNode);
            }
            TryDirection(queue, currNode, "U", 0, -1);
            TryDirection(queue, currNode, "R", 1, 0);
            TryDirection(queue, currNode, "D", 0, 1);
            TryDirection(queue, currNode, "L", -1, 0);
        }

        return null;
    }
    private static void TryDirection(Queue<Point> queue, Point currNode, string direction, int deltaX, int deltaY)
    {
        int newX = currNode.X + deltaX;
        int newY = currNode.Y + deltaY;
        if(newX >= 0 && newX < width && newY >= 0 && newY < height && 
            labyrinth[newY, newX] == '-')
        {
            labyrinth[newY, newX] = VisitedCell;
            var nextPoint = new Point()
            {
                X = newX,
                Y = newY,
                Direction = direction,
                PreviousPoint = currNode
            };
            queue.Enqueue(nextPoint);
        }
    }
    private static string TracePathBack(Point currNode)
    {
        var path = new StringBuilder();
        while(currNode.PreviousPoint != null)
        {
            path.Append(currNode.Direction);
            currNode = currNode.PreviousPoint;
        }
        var pathReversed = new StringBuilder(path.Length);
        for (int i = path.Length - 1; i >= 0; i--)
        {
            pathReversed.Append(path[i]);
        }

        return pathReversed.ToString();
    }
    private static bool IsEXit(Point currNode)
    {
        if (currNode.X == width - 1 || currNode.X == 0 ||
            currNode.Y == 0 || currNode.Y == height - 1)
            return true;

        return false;
    }
    public static void ReadLabyrinth()
    {
        width = int.Parse(Console.ReadLine());
        height = int.Parse(Console.ReadLine());
        labyrinth = new char[height, width];
        for(int row = 0; row < height; row++)
        {
            char[] rowMembers = Console.ReadLine().ToCharArray();
            for(int column = 0; column < width; column++)
            {
                labyrinth[row, column] = rowMembers[column];
            }            
        }
    }
    public static void Main()
    {
        ReadLabyrinth();
        string shortestPathToExit = FindShortestWayToExit();
        if (shortestPathToExit == null)
        {
            Console.WriteLine("No exit!");
        }
        else if(shortestPathToExit == "")
        {
            Console.WriteLine("Start is at the exit.");
        }
        else
        {
            Console.WriteLine("Shortest exit: " + shortestPathToExit);
        }
    }
}
