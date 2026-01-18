using System.Collections.Generic;

namespace MathUtils;

public static class Primes
{
    public static bool IsPrime(long number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (long)Math.Floor(Math.Sqrt(number));

        for (long i = 3; i <= boundary; i += 2)
        {
            if (number % i == 0) return false;
        }

        return true;
    }
}

public class PrimeGenerator
{
    private List<long> _foundPrimes;

    public PrimeGenerator()
    {
        _foundPrimes = new List<long> { 2, 3 };
    }

    public List<long> AllPrimes => _foundPrimes;

    // This checks if a number IS prime using what we currently know
    public bool CheckIfPrime(long number)
    {
        // If the number is already in our list, it's prime!
        if (number <= _foundPrimes[^1])
        {
            return _foundPrimes.Contains(number);
        }

        // If it's bigger than our list, we must grow the list first
        while (_foundPrimes[^1] < number)
        {
            GetNextPrime();
        }

        // Now we have enough primes to check via trial division
        double boundary = Math.Sqrt(number);
        foreach (long p in _foundPrimes)
        {
            if (p > boundary) break;
            if (number % p == 0) return false;
        }
        return true;
    }

    public long GetNextPrime()
    {
        // Start 2 steps ahead of the last prime (skipping evens)
        long candidate = _foundPrimes[^1] + 2; 

        while (true)
        {
            // We use a simplified internal check here to avoid the recursion loop
            if (InternalCheck(candidate))
            {
                _foundPrimes.Add(candidate);
                return candidate;
            }
            candidate += 2;
        }
    }

    // A "Private" helper that only looks at the current list
    private bool InternalCheck(long number)
    {
        double boundary = Math.Sqrt(number);
        foreach (long p in _foundPrimes)
        {
            if (p > boundary) break;
            if (number % p == 0) return false;
        }
        return true;
    }
}

public static class Squares
{
    public static bool IsEven(long number)
    {
        if ((number & 1) == 0)
        {
            return true;
        }
        return false;
    }

    public static bool IsSquare(long n)
    {
        if (n < 0) return false;
        if (n == 0) return true;

        long x = n;
        long y = (x + 1) / 2;
        while (y < x)
        {
            x = y;
            y = (x + n / x) / 2;
        }
        
        return x * x == n;
    }
}