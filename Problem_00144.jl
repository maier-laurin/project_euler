using StaticArrays
using LinearAlgebra

mutable struct Line
    point::SVector{2, Float64}
    direction::SVector{2, Float64}

    Line(point::SVector{2, Float64}, direction::SVector{2, Float64}) = begin
        normalized_direction = normalize(direction)  # Normalize direction
        new(point, normalized_direction)
    end

    Line(point::AbstractVector{Float64}, direction::AbstractVector{Float64}) = begin
        Line(SVector{2, Float64}(point), SVector{2, Float64}(direction))
    end
end

struct Ellips
    weights::SVector{2, Float64}
    radius::Float64
    halfaxis::SVector{2, Float64}

    Ellips(weights::SVector{2, Float64}, radius::Float64) = begin
        a = sqrt(radius/weights[1])
        b = sqrt(radius/weights[2])
        axis = SVector{2, Float64}([a, b])
        new(weights, radius, axis)
    end

    Ellips(weights::AbstractVector{Float64}, radius::Number) = begin
        Ellips(SVector{2, Float64}(weights), Float64(radius))
    end
end

function eval(lin::Line, λ::Number)::SVector{2, Float64}
    x1 = lin.point[1]+λ*lin.direction[1]
    x2 = lin.point[2]+λ*lin.direction[2]
    return SVector{2}([x1, x2])
end

function intersect(ell::Ellips, lin::Line)
    #* generic function giving the intersectionpoints between a line and an ellipse
    γ = ell.weights[1]*lin.point[1]^2+ell.weights[2]*lin.point[2]^2-ell.radius
    β = 2*(ell.weights[1]*lin.point[1]*lin.direction[1]+ell.weights[2]*lin.point[2]*lin.direction[2])
    α = ell.weights[1]*lin.direction[1]^2+ell.weights[2]*lin.direction[2]^2
    sq = β^2-4*α*γ
    if sq < 0
        return Vector{SVector{2, Float64}}[]
    elseif sq == 0
        return[eval(lin, -β/(2*α))]
    else
        sqq = sqrt(sq)
        λ1 = (-β+sqq)/(2*α)
        λ2 = (-β-sqq)/(2*α)
        return [eval(lin, λ1), eval(lin, λ2)]
    end
end
function intersect(lin::Line, ell::Ellips)
    return intersect(ell, lin)
end
function intersect(ell::Ellips, lin::Line, reflectionbeam::Bool)
    #* optimiesed version where we know one point lies on the border, and the beam stays inside
    if reflectionbeam
        β = 2*(ell.weights[1]*lin.point[1]*lin.direction[1]+ell.weights[2]*lin.point[2]*lin.direction[2])
        α = ell.weights[1]*lin.direction[1]^2+ell.weights[2]*lin.direction[2]^2
        λ = -β/α
        return [eval(lin, λ)]
    end
    return intersect(ell, lin) 
end

function normal_vec(ell::Ellips, x::SVector{2, Float64})
    #*finds the direction normal to an ellips at an point on the border
    x1 = ell.weights[1]*2*x[1]
    x2 = ell.weights[2]*2*x[2]
    return SVector{2, Float64}(normalize([x1, x2]))
end

function bounce(v::SVector{2, Float64}, n::SVector{2, Float64})::SVector{2, Float64} 
    nc = (v[1]*n[1]+v[2]*n[2])*n
    return SVector{2, Float64}(2*nc-v)
end

function bouncecount()::Int64
    e = Ellips([4.0, 1.0], 100)
    surface_normal = -normal_vec(e, SVector{2, Float64}([1.4,-9.6]))
    ray = Line([1.4,-9.6], [1.4,-9.6-10.1])
    ray.direction = bounce(-ray.direction, surface_normal)
    n = 1
    # start bouncing
    while ray.point[2] < 9 || ray.point[1] < -0.01 || ray.point[1] > 0.01
        ray.point = intersect(e, ray, true)[1]
        surface_normal = -normal_vec(e, ray.point)
        ray.direction = bounce(-ray.direction, surface_normal)
        n = n + 1
    end
    return n - 1
end

@timev bouncecount()