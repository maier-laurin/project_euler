function eq_ff(x::UInt64, y::UInt64, n::UInt64)::Tuple{Int64, Int64, Int64}
    left = n*(x+y)
    right = x*y
    if left == right
        return (1, -1, 1)
    elseif left < right
        return (0, -1, 0)
    else
        return (0, 0, 1)
    end
end

function find_n(m)::Int64
    n::UInt64 = 1
    x::UInt64 = 0
    y::UInt64 = 0

    c_n::UInt64 = 0
    while c_n <= m
        c_n = 0
        x = y = n*2
        while x > n
            tub = eq_ff(x, y, n)
            c_n = c_n + tub[1]
            x = x + tub[2]
            y = y + tub[3]
        end
        n = n + 1
        println(string(n) * " ==> " * string(c_n))
    end
    return n - 1
end

@timed find_n(1000)