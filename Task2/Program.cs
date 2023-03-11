using Kse.Algorithms.Samples;
var List = new List<Point>();
List.Add(new Point(0,0));
List.Add(new Point(88,32));
var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 35,
    Width = 90,
   Seed = 2,
    Noise = .1f,
    AddTraffic = true,
    TrafficSeed = 1234
});

string[,] map = generator.Generate();
//map[List[0].Column, List[0].Row] = " ";
//map[List[1].Column, List[1].Row] = " ";
new MapPrinter().Print(map, List,FindShortestPath(map, List[0],List[1]));
Console.WriteLine("---------");

new MapPrinter().Print(map, List,FindDijkstra(map, List[0],List[1]));
List<Point> FindShortestPath(string[,] map, Point start, Point end)
    {
        var state = start;
        
        var distances = new Dictionary<Point, Int64>();
        distances[state] = 0;
        
        var previous = new Dictionary<Point, Point>();
        previous[state] = state;
        double Time = 0.0;
        while (!(state.Row==end.Row && state.Column==end.Column))
        {
            var neoghbors = GetNeigbors(state, map);
            foreach (var VAR in neoghbors)
            {
                if (!previous.ContainsKey(VAR))
                {
                    var Zator = map[VAR.Column, VAR.Row];
                   
                
                    
                 
                    var Pif = Math.Pow(state.Column - end.Column, 2) + Math.Pow(state.Row - end.Row, 2);
                   var distance = Math.Sqrt(Pif);
                   
                    distances[VAR] = distances[state]+int.Parse(Zator)+(long)distance;
                    previous[VAR] = state;
                }
            }
            distances.Remove(state);
            if (distances.Count==0)
            {
                Console.WriteLine("BRUH");
                break;
            }
            state=distances.MinBy(kvp => kvp.Value).Key;
        }
        
        List<Point> path = new List<Point>();
        while (state.Row!=start.Row || state.Column!=start.Column)
        { 
            var Zator = map[state.Column, state.Row];
            path.Add(state);
            state = previous[state];
           
            // Zator = Convert.ToInt32(new string(Zator,1));
                
            //Console.WriteLine(Zator);
            var V = 60 - (int.Parse(Zator) - 1) * 6;
                Time += 1.0 / V;
            
        }

        foreach (var VARIABLE in path)
        {
            Console.WriteLine("({0},{1})",VARIABLE.Column,VARIABLE.Row);
           
            
        }
        Console.WriteLine("Time Is:");
        Console.WriteLine(Time);
        return path;
    }


List<Point> GetNeigbors(Point start, string[,] map)
{
    int numRows = map.GetLength(1);
    int numCols = map.GetLength(0);
    var Neighbor = new List<Point>();
    //index of node
    if (start.Row > 0 && map[start.Column, start.Row - 1] != "█")
    {
        Point node2 = new Point(start.Column, start.Row - 1);
        Neighbor.Add(node2);
    }

    if (start.Row < numRows - 1 && map[start.Column, start.Row + 1] != "█")
    {
        Point node2 = new Point(start.Column, start.Row + 1);
        Neighbor.Add(node2);
    }

    if (start.Column > 0 && map[start.Column - 1, start.Row] != "█")
    {
        Point node2 = new Point(start.Column - 1, start.Row);
        Neighbor.Add(node2);
    }

    if (start.Column < numCols - 1 && map[start.Column + 1, start.Row] != "█")
    {
        Point node2 = new Point(start.Column + 1, start.Row);
        Neighbor.Add(node2);
    }

    return Neighbor;
}




List<Point> FindDijkstra(string[,] map, Point start, Point end)
    {
        var state = start;
        
        var distances = new Dictionary<Point, Int64>();
        distances[state] = 0;
        
        var previous = new Dictionary<Point, Point>();
        previous[state] = state;
        double Time = 0.0;
        while (!(state.Row==end.Row && state.Column==end.Column))
        {
            var neoghbors = GetNeigborsD(state, map);
            foreach (var VAR in neoghbors)
            {
                if (!previous.ContainsKey(VAR))
                {
                    var Zator = map[VAR.Column, VAR.Row];
                   
                
                    
                 
                  //  var Pif = Math.Pow(state.Column - end.Column, 2) + Math.Pow(state.Row - end.Row, 2);
                  // var distance = Math.Sqrt(Pif);
                   
                    distances[VAR] = distances[state]+int.Parse(Zator);
                    previous[VAR] = state;
                }
            }
            distances.Remove(state);
            if (distances.Count==0)
            {
                Console.WriteLine("BRUH");
                break;
            }
            state=distances.MinBy(kvp => kvp.Value).Key;
        }
        
        List<Point> path = new List<Point>();
        while (state.Row!=start.Row || state.Column!=start.Column)
        { 
            var Zator = map[state.Column, state.Row];
            path.Add(state);
            state = previous[state];
           
            // Zator = Convert.ToInt32(new string(Zator,1));
                
            //Console.WriteLine(Zator);
            var V = 60 - (int.Parse(Zator) - 1) * 6;
                Time += 1.0 / V;
            
        }

        foreach (var VARIABLE in path)
        {
            Console.WriteLine("({0},{1})",VARIABLE.Column,VARIABLE.Row);
           
            
        }
        Console.WriteLine("Time Is:");
        Console.WriteLine(Time);
        return path;
    }


List<Point> GetNeigborsD(Point start, string[,] map)
{
    int numRows = map.GetLength(1);
    int numCols = map.GetLength(0);
    var Neighbor = new List<Point>();
    //index of node
    if (start.Row > 0 && map[start.Column, start.Row - 1] != "█")
    {
        Point node2 = new Point(start.Column, start.Row - 1);
        Neighbor.Add(node2);
    }

    if (start.Row < numRows - 1 && map[start.Column, start.Row + 1] != "█")
    {
        Point node2 = new Point(start.Column, start.Row + 1);
        Neighbor.Add(node2);
    }

    if (start.Column > 0 && map[start.Column - 1, start.Row] != "█")
    {
        Point node2 = new Point(start.Column - 1, start.Row);
        Neighbor.Add(node2);
    }

    if (start.Column < numCols - 1 && map[start.Column + 1, start.Row] != "█")
    {
        Point node2 = new Point(start.Column + 1, start.Row);
        Neighbor.Add(node2);
    }

    return Neighbor;
}