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
                int node1 = row * numCols + col;
                //index of node
                if (row > 0 && map[row - 1, col] != "█")
                {
                    int node2 = (row - 1) * numCols + col;
                    graph[node1, node2] = 1;
                    graph[node2, node1] = 1;
                }

                if (row < numRows - 1 && map[row + 1, col] != "█")
                {
                    int node2 = (row + 1) * numCols + col;
                    graph[node1, node2] = 1;
                    graph[node2, node1] = 1;
                }

                if (col > 0 && map[row, col - 1] != "█")
                {
                    int node2 = row * numCols + col - 1;
                    graph[node1, node2] = 1;
                    graph[node2, node1] = 1;
                }

                if (col < numCols - 1 && map[row, col + 1] != "█")
                {
                    int node2 = row * numCols + col + 1;
                    graph[node1, node2] = 1;
                    graph[node2, node1] = 1;
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
            distances[i] = int.MaxValue;
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
                if (!visited[j] && graph[current, j] != int.MaxValue && distances[current] + graph[current, j] < distances[j])
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



