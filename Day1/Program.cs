const string filePath = "../../../input.txt";


var numberWords = new Dictionary<string, int>
{
    { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 },
    { "five", 5 },
    { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 }
};

List<int> ExtractNumbers(string str)
{
    return str.Where(char.IsNumber).Select(c => int.Parse(c.ToString())).ToList();
}

int? ExtractNumFromText(string str)
{
    foreach (var numberWordsKey in numberWords!.Keys.Where(str.Contains)) return numberWords[numberWordsKey];
    return null;
}

void PartOne()
{
    var sum = 0;
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
}

void PartTwo()
{
    var sum = 0;
    if (File.Exists(filePath))
    {
        var streamReader = new StreamReader(filePath);
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (line != null)
            {
                string textSoFar = "";
                int firstNumber = 0;
                int lastNumber = 0;
                foreach (var c in line)
                {
                    textSoFar+= c;
                    var num = ExtractNumFromText(textSoFar);
                    if (num != null)
                    {
                        if(firstNumber == 0)
                            firstNumber = num.Value;
                        else
                            lastNumber = num.Value;
                        textSoFar = textSoFar.Remove(0, textSoFar.Length-1);
                    } else if (char.IsNumber(c))
                    {
                        if (firstNumber == 0)
                            firstNumber = int.Parse(c.ToString());
                        else
                            lastNumber = int.Parse(c.ToString());
                    }
                }

                if (lastNumber == 0)
                {
                    lastNumber = firstNumber;
                }
                sum += int.Parse($"{firstNumber}{lastNumber}");
            }
        }
    }

    Console.WriteLine(sum);
}
PartOne();
PartTwo();