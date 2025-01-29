function check_double_pandigital(number::BigInt, digs::Vector{Int8})
    digits!(digs, number)
    msi = findlast(x -> x != 0, digs) # index of most significant digit
    if msi >= length(digs)-1
        append!(digs, zeros(Int8, length(digs))) # doubling the available digits memory
    end
    if msi < 9
        return false
    end
    first_part = digs[(msi-8):msi]
    last_part = digs[1:9]
    if all(i -> i in first_part, Base.OneTo(9))
        #using short circuid all for more performance
        if all(i -> i in last_part, Base.OneTo(9))
            return true
        end
    end
    return false
end

function find_first()
    Fn_2::BigInt = 0 # the fibonacy number 2 steps ago
    Fn_1::BigInt = 1 # the fibonacy number 1 step ago
    Fn::BigInt = 1 # the current fibonacy number
    n::BigInt = 1 # counter
    digits_memory = zeros(Int8, 2) # thempory location for the digitised BigInt
    while !check_double_pandigital(Fn, digits_memory)
        Fn_2 = Fn_1
        Fn_1 = Fn
        Fn = Fn_2 + Fn_1
        n = n + 1
    end
    return (n, length(digits(Fn)))
end

@timev find_first()