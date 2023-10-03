namespace DadJokesBattle;
class Program
{
    static void Main(string[] args)
    {
        var battle = new Battle();
        battle.Play();
    }
}

class Battle
{
    private int _firstComedianScore;
    private int _secondComedianScore;
    private int _totalRoundNumber;
    private List<string> _jokes;

    public Battle()
    {
        _firstComedianScore = 0;
        _secondComedianScore = 0;
        _jokes = ReadJokesFromFile(@"..\..\..\Jokes.txt");
        _totalRoundNumber = _jokes.Count / 2;
    }

    public void Play()
    {
        var round = 1;
        var random = new Random();
        while (_jokes.Count > 1)
        {
            PrintHeader(round);
            var jokeNumber = random.Next(_jokes.Count);
            ShowJoke(1, jokeNumber);
            _firstComedianScore += GiveScore(1);
            Console.Clear();

            PrintHeader(round);
            ShowJoke(1, jokeNumber);
            _jokes.RemoveAt(jokeNumber);

            jokeNumber = random.Next(_jokes.Count);
            ShowJoke(2, jokeNumber);
            _secondComedianScore += GiveScore(2);
            _jokes.RemoveAt(jokeNumber);
            round++;
            Console.Clear();
        }
        ShowResult();
    }

    private void ShowJoke(int comedianNumber, int jokeNumber)
    {
        if (comedianNumber == 1)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"Комик {comedianNumber} | Балл: {_firstComedianScore}");
        }
        else
        {
            Console.SetCursorPosition(0, 6);
            Console.WriteLine($"Комик {comedianNumber} | Балл: {_secondComedianScore}");
        }
        Console.WriteLine(_jokes[jokeNumber]);
        //var joke = _jokes[jokeNumber].Split(new char[] { '.', '!', '?' }, 2, StringSplitOptions.RemoveEmptyEntries);
        //Console.WriteLine(joke[0]);
        //Thread.Sleep(2000);
        //Console.WriteLine(joke[1].Trim());
    }

    private int GiveScore(int comedianNumber)
    {
        Console.SetCursorPosition(0, 10);
        Console.WriteLine(new String('_', 40));
        Console.WriteLine($"\nПоставьте оценку {comedianNumber} шутке от 0 до 5");
        var userInput = Console.ReadLine();
        if (int.TryParse(userInput, out int score) && score >= 0 && score <= 5)
        {
            return score;
        }
        else
        {
            Console.WriteLine("Неверный ввод! Игрок остался без баллов :С");
            Console.ReadKey();
            return 0;
        }
    }

    private void ShowResult()
    {
        Console.WriteLine(new string('*', 10) + " Баттл завершен! " + new string('*', 10));
        Console.WriteLine($"\nКомик 1: {_firstComedianScore} | Комик 2: {_secondComedianScore}");
        if (_firstComedianScore > _secondComedianScore)
            Console.WriteLine("\n*** Первый победил! ***");
        else if (_secondComedianScore > _firstComedianScore)
            Console.WriteLine("\n*** Второй победил! ***");
        else
            Console.WriteLine("\n*** Ничья! ***"); ;
    }

    private void PrintHeader(int round)
    {
        Console.WriteLine(new string(' ', 15) + $" РАУНД {round} / {_totalRoundNumber} " + new string(' ', 15));
    }

    private List<string> ReadJokesFromFile(string path)
    {
        var jokes = new List<string>();

        using (StreamReader reader = new StreamReader(path))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line != "") jokes.Add(line);
            }
        }
        return jokes;
    }
}