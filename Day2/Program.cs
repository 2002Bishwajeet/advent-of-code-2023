const string filePath = "../../../input.txt";


var goalCubes = new List<Cube>
{
    new("red", 12),
    new("green", 13),
    new("blue", 14)
};

int PartOne()
{
    var games = new List<Game>();
    if (File.Exists(filePath))
    {
        var streamReader = new StreamReader(filePath);
        var sum = 0;
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (line != null)
            {
                var gameNo = int.Parse(line.Split(":").First().Split(" ").Last());
                var isGoalCube = true;
                var seperators = line.Split(":").Last().Split(";");
                foreach (var seperator in seperators)
                {
                    var cubes = seperator.Trim().Split(", ");
                    foreach (var cube in cubes)
                    {
                        var cubeName = cube.Split(" ").Last();
                        var cubeCount = int.Parse(cube.Split(" ").First());
                        var newcube = new Cube(cubeName, cubeCount);
                        if (goalCubes.Any(cube1 => cube1.Name == cubeName && cube1.Count < cubeCount))
                        {
                            isGoalCube = false;
                            break;
                        }
                    }
                }

                if (isGoalCube) sum += gameNo;
            }
        }

        return sum;
    }

    return -1;
}

int PartTwo()
{
    var games = new List<Game>();
    if (File.Exists(filePath))
    {
        var streamReader = new StreamReader(filePath);
        var sum = 0;
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (line != null)
            {
                var gameNo = int.Parse(line.Split(":").First().Split(" ").Last());

                var seperators = line.Split(":").Last().Split(";");
                var subsets = new List<Cube>();
                foreach (var seperator in seperators)
                {
                    var cubes = seperator.Trim().Split(", ");
                    foreach (var cube in cubes)
                    {
                        var cubeName = cube.Split(" ").Last();
                        var cubeCount = int.Parse(cube.Split(" ").First());
                        if(subsets.Count !=0 && subsets.Any(cube1 => cube1.Name == cubeName))
                        {
                            subsets.First(cube1 => cube1.Name == cubeName).SetMaxCount(cubeCount);
                        }
                        else
                        {
                            subsets.Add(new Cube(cubeName, cubeCount));
                        }
                    }
                }
                games.Add(new Game(gameNo,subsets));
            }
        }

        foreach (var game in games)
        {
            sum += game.GetScore();
        }

        return sum;
    }

    return -1;
}

Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

internal class Game
{
    public Game(int id, IEnumerable<Cube> subsets)
    {
        Id = id;
        Subsets = subsets;
    }

    public int Id { get; }
    public IEnumerable<Cube> Subsets { get; set; }
    
    public int GetScore()
    {
        var score = 1;
        foreach (var subset in Subsets)
        {
            score *= subset.Count;
        }

        return score;
    }
}

internal class Cube
{
    public Cube(string name, int count)
    {
        Name = name;
        Count = count;
    }

    public int Count { get; internal set; }

    public string Name { get; }

    public void SetCount(int count)
    {
        Count += count;
    }

    public void SetMaxCount(int count)
    {
        Count = int.Max(Count, count);
    }
}