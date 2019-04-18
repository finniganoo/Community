// Recursive isprime function.
let isprime n =
    let rec check i =
        i > n/2 || (n % i <> 0 && check (i + 1))
    check 2

let aSequence = seq { for n in 1..10 do if isprime n then yield n }
for x in aSequence do
    printfn "%d" x



// greatest common divisor

open System

let rec gcd x y =
    if y = 0 then x
    else gcd y (x % y)
    
Console.WriteLine(gcd 259 111) // prints 37
