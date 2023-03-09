using Kse.Algorithms.Samples;
var List = new List<Point>();
List.Add(new Point(0,0));
List.Add(new Point(4,4));
var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 35,
    Width = 90,
});

string[,] map = generator.Generate();
new MapPrinter().Print(map, List,FindShortestPath(map, List[0],List[1]));

//FindShortestPath(map, List[0],List[1]);


 List<Point> FindShortestPath(string[,] map, Point start, Point end)
    {
        int numRows = map.GetLength(0);
        int numCols = map.GetLength(1);
        int[,] graph = new int[numRows * numCols, numRows * numCols];

        for (int i = 0; i < numRows * numCols; i++)
        {
            for (int j = 0; j < numRows * numCols; j++)
            {
                graph[i, j] = 999999999;
            }
        }

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                if (map[row, col] == "█")
                {
                    continue;
                }
//index of node
                Point node1 = new Point(col, row);
                //index of node
                if (row > 0 && map[row - 1, col] != "█")
                {
                    Point node2 = new Point(col, row - 1);
                    int index1 = node1.Row * numCols + node1.Column;
                    int index2 = node2.Row * numCols + node2.Column;
                    graph[index1, index2] = 1;
                    graph[index2, index1] = 1;
                }

                if (row < numRows - 1 && map[row + 1, col] != "█")
                {
                    Point node2 = new Point(col, row + 1);
                    int index1 = node1.Row * numCols + node1.Column;
                    int index2 = node2.Row * numCols + node2.Column;
                    graph[index1, index2] = 1;
                    graph[index2, index1] = 1;
                }

                if (col > 0 && map[row, col - 1] != "█")
                {
                    Point node2 = new Point(col - 1, row);
                    int index1 = node1.Row * numCols + node1.Column;
                    int index2 = node2.Row * numCols + node2.Column;
                    graph[index1, index2] = 1;
                    graph[index2, index1] = 1;
                }

                if (col < numCols - 1 && map[row, col + 1] != "█")
                {
                    Point node2 = new Point(col + 1, row);
                    int index1 = node1.Row * numCols + node1.Column;
                    int index2 = node2.Row * numCols + node2.Column;
                    graph[index1, index2] = 1;
                    graph[index2, index1] = 1;
                }
            }
        }

        int startNode = start.Row * numCols + start.Column;
        int endNode = end.Row * numCols + end.Column;

        int[] distances = new int[numRows * numCols];
        bool[] visited = new bool[numRows * numCols];
        int[] previous = new int[numRows * numCols];

        for (int i = 0; i < numRows * numCols; i++)
        {
            distances[i] = 999999999;
            visited[i] = false;
            previous[i] = -1;
        }

        distances[startNode] = 0;

        for (int i = 0; i < numRows * numCols - 1; i++)
        {
            int current = MinDistance(distances, visited);
            visited[current] = true;

            for (int j = 0; j < numRows * numCols; j++)
            {
                if (!visited[j] && graph[current, j] != 999999999 && distances[current] + graph[current, j] < distances[j])
                {
                    distances[j] = distances[current] + graph[current, j];
                    previous[j] = current;
                }
            }
        }

        List<Point> path = new List<Point>();

        int node = endNode;
        while (previous[node] != -1)
        {
            int row = node / numCols;
            int col = node % numCols;
            path.Insert(0, new Point(col, row));
            node = previous[node];
        }

        path.Insert(0, start);
        
        foreach (var point in path)
        {
            Console.WriteLine("({0}, {1})", point.Row, point.Column);
        }
       int MinDistance(int[] distances, bool[] visited)
        {
            int min = 999999999;
            int minIndex = 0;

            for (int i = 0; i < distances.Length; i++)
            {
                if (!visited[i] && distances[i] <= min)
                {
                    min = distances[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }

        return path;
    }



