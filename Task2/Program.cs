
using Kse.Algorithms.Samples;

var List = new List<Point>();
List.Add(new Point(0,0));
List.Add(new Point(15,5));

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 35,
    Width = 90,
    Noise = 0F
});

string[,] map = generator.Generate();
new MapPrinter().Print(map,List);

//Console.WriteLine("------------");
//PaintBFS(new Point(6, 5));
GetShortestPath(map,new Point(0,9),new Point(0,10));
List<Point> GetShortestPath(string[,] map, Point start, Point goal)
{
   var MinimalDist = int.MaxValue;
    int[,] distances = new int[map.GetLength(0), map.GetLength(1)];
    Point[,] origins = new Point[map.GetLength(0), map.GetLength(1)];
    var Q = new HashSet<Point>();
    // your code here
    foreach (Point p in convertToPoint(map))
    {
        distances[p.Row, p.Column] = 999999999;
        origins[p.Row, p.Column] = new Point(p.Column, p.Row);
        Q.Add(p);
    }

    distances[0, 9] = 0;

    while (Q.Count>0)
    {
        Point min = new Point();
        foreach (Point v in Q)
        {
            if (distances[v.Row, v.Column] < MinimalDist)
            {
                MinimalDist = distances[v.Row, v.Column];
                min = new Point(v.Column, v.Row);
            }
        }

        Q.Remove(min);
        foreach (var P in Q)
        {
            if (GetNeighbours(P.Column, P.Row, map, 1, true)!=null)
            {
                int alt = distances[min.Column,min.Row] + distances[GetNeighbours(P.Column, P.Row, map, 1, true)];
            }
        }
    }

    return null;
}

List<Point> convertToPoint(string[,] map)
{
    var ToPoint = new List<Point>();
    for (var row = 0; row < map.GetLength(1); row++)
    {
     //   Console.Write($"{row}\t");
        for (var column = 0; column < map.GetLength(0); column++)
        {
            ToPoint.Add(new Point(column,row));
        }
    }

    return ToPoint;
}
/*function Dijkstra(Graph, source):
2      
3      for each vertex v in Graph.Vertices:
4          dist[v] ← INFINITY
5          prev[v] ← UNDEFINED
6          add v to Q
7      dist[source] ← 0
8      
9      while Q is not empty:
10          u ← vertex in Q with min dist[u]
11          remove u from Q
12          
13          for each neighbor v of u still in Q:
14              alt ← dist[u] + Graph.Edges(u, v)
15              if alt < dist[v]:
16                  dist[v] ← alt
17                  prev[v] ← u
18
19      return dist[], prev[]
*/

 List<Point> GetNeighbours(int column, int row, string[,] maze, int offset = 2,
    bool checkWalls = false)
{
    var result = new List<Point>();
    TryAddWithOffset(offset, 0);
    TryAddWithOffset(-offset, 0);
    TryAddWithOffset(0, offset);
    TryAddWithOffset(0, -offset);
    return result;

    void TryAddWithOffset(int offsetX, int offsetY)
    {
        var newColumn = column + offsetX;
        var newRow = row + offsetY;
        if (newColumn >= 0 && newRow >= 0 && newColumn < maze.GetLength(0) && newRow < maze.GetLength(1))
        {
            if (!checkWalls || maze[newColumn, newRow] == ".")
            {
                result.Add(new Point(newColumn, newRow));
            }
        }
    }
}