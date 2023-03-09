using Kse.Algorithms.Samples;
var List = new List<Point>();
List.Add(new Point(0,0));
List.Add(new Point(8,7));
var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 35,
    Width = 90,
});

string[,] map = generator.Generate();
map[List[0].Row, List[0].Column] = " ";
map[List[1].Row, List[1].Column] = " ";
new MapPrinter().Print(map, List,FindShortestPath(map, List[0],List[1]));
List<Point> FindShortestPath(string[,] map, Point start, Point end)
    {
        var state = start;
        
        var distances = new Dictionary<Point, Int64>();
        distances[state] = 0;
        
        var previous = new Dictionary<Point, Point>();
        previous[state] = state;
        while (state.Row!=end.Row || state.Column!=end.Column)
        {
            var neoghbors = GetNeigbors(state, map);
            foreach (var VAR in neoghbors)
            {
                if (!previous.ContainsKey(VAR))
                {
                    distances[VAR] = distances[state]+1;
                    previous[VAR] = state;
                    
                }
            }
            distances.Remove(state);
            if (distances.Count==0)
            {
                break;  
            }
            state=distances.MinBy(kvp => kvp.Value).Key;
           
           
        }
        List<Point> path = new List<Point>();
        while (state.Row!=start.Row || state.Column!=start.Column)
        {
            path.Add(state);
            state = previous[state];
            
        }

        foreach (var VARIABLE in path)
        {
            Console.WriteLine("({0},{1})",VARIABLE.Row,VARIABLE.Column);
        }
        return path;
    }


List<Point> GetNeigbors(Point start,string[,] map)
{
    int numRows = map.GetLength(0);
    int numCols = map.GetLength(1);
    var Neighbor = new List<Point>();
    //index of node
    if (start.Row > 0 && map[start.Row - 1, start.Column] != "█")
    {
        Point node2 = new Point(start.Column, start.Row - 1);
        Neighbor.Add(node2);
               
    }

    if (start.Row < numRows - 1 && map[start.Row + 1, start.Column] != "█")
    {
        Point node2 = new Point(start.Column, start.Row + 1);
        Neighbor.Add(node2);
             
    }

    if (start.Column > 0 && map[start.Row, start.Column - 1] != "█")
    {
        Point node2 = new Point(start.Column - 1, start.Row);
        Neighbor.Add(node2);
              
    }

    if (start.Column < numCols - 1 && map[start.Row, start.Column + 1] != "█")
    {
        Point node2 = new Point(start.Column + 1, start.Row);
        Neighbor.Add(node2);
    }

    return Neighbor;
}
