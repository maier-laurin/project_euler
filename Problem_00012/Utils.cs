namespace MathUtils;

public class TriangleNumber
{
    private long _th;
    private long _value;

    public TriangleNumber()
    {
        _th = 1;
        _value = 1;
    }

    public TriangleNumber(long i)
    {
        if (i < 1) throw new ArgumentOutOfRangeException(nameof(i), "Index must be 1 or greater.");
        _th = i;
        _value = (long)i * (i + 1) / 2;
    }

    public long th => _th;
    public long value => _value;

    public void next()
    {
        _th++;
        _value += th;
    }
}

public static class Factors
{
    public static List<long> factor(long i)
    {
        List<long> factors = new List<long>();
        for (long q = 1; q <= Math.Sqrt(i); q++)
        {
            if (i % q == 0)
            {
                factors.Add(q);
                factors.Add(i/q);
                //! will add one to many if the number is a square number, but the probarbility for that is diminishing
            }
        }
        return factors;
    }
}