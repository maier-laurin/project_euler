using MathUtils;

Console.WriteLine("problem 46 started");

PrimeGenerator gen = new();
long cand = 3;
while (Goldbach(cand, gen))
{
    if(gen.AllPrimes[^1] <= cand)
    {
        gen.GetNextPrime();
    }
    cand += 2;
}

bool Goldbach(long number, PrimeGenerator Primes)
{
    if(Squares.IsEven(number))
    {
        Console.WriteLine($"{number} is even!");
        return true;
    }
    List<long> a = Primes.AllPrimes;
    for (int i = 0; i < a.Count; i++)
    {
        long prime = a[i];
        long remainder = number - prime;
        if (remainder < 0)
        {
            Console.WriteLine($"{number} defies the Goldbach Conjecture");
            return false;
        }
        if (Squares.IsEven(remainder))
        {
            if(Squares.IsSquare(remainder / 2))
            {
                Console.WriteLine($"{number} = {prime} + 2 * {Math.Sqrt(remainder/2)}²");
                return true;
            }
        }
    }
    throw new Exception("Something unexpected happend!");
}