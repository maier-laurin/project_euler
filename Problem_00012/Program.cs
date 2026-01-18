using MathUtils;

TriangleNumber tn = new TriangleNumber();
List<long> fcts = Factors.factor(tn.value);
long mxcnt = 0;
while (mxcnt < 500){
    tn.next();
    fcts = Factors.factor(tn.value);
    if (fcts.Count > mxcnt)
    {
        mxcnt = fcts.Count;
        Console.WriteLine($"{tn.value} has {fcts.Count} factors");
        ////Console.WriteLine(mxcnt);
    }
    
}

Console.WriteLine(tn.value);

