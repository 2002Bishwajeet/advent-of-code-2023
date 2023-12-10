const string filePath = "../../../input.txt";


 string[] words = {"one","two","three","four","five","six","seven","eight","nine"};

List<int> ExtractNumbers(String str)
{
    // var numbers = new List<int>();
    // str.Where(words)
    // str.Contains(words);
    return str.Where(char.IsNumber).Select(c => int.Parse(c.ToString())).ToList();
}

int sum = 0;

if (File.Exists(filePath))
{
    var streamReader = new StreamReader(filePath);
    while (!streamReader.EndOfStream)
    {
        var line = streamReader.ReadLine();
        if (line != null)
        {
            Console.WriteLine(line);
            var numbers = ExtractNumbers(line);
            switch (numbers.Count)
            {
                case > 1:
                {
                    sum += int.Parse($"{numbers.First()}{numbers.Last()}");
                    break;
                }
                case 1:
                    //repeat the number twice as 2 digit
                    sum += int.Parse($"{numbers.First()}{numbers.First()}");
                    break;
            }
        }
    }
}

Console.WriteLine(sum);