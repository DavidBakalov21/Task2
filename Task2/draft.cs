/*
 var visited = new List<Point>();
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
               // int alt = distances[min.Column,min.Row] + distances[GetNeighbours(P.Column, P.Row, map, 1, true)];
            }
        }
    }

    return null;
    
    void Visit(Point point)
    {
        map[point.Column, point.Row] = "f";
        visited.Add(point);
    }
*/