open System
open Engine
open ConsoleRenderer
open Bots

[<EntryPoint>]
let main argv =
    let random = new System.Random()
    let size = (70, 35)
    let bots = [wriggler; wrigglerL; loony; loony; kingOfTheNorth; wriggler]
               |> List.mapi (fun i b -> (i, b))

    let history = game random size bots |> Seq.toList

    history |> render size

    let survivors = (history |> Seq.last).Heads

    match survivors.Length with
    | 0 -> Console.Write("  !DRAW!  ")
    | 1 -> Console.Write("<- WINNER!  ")
    | _ -> ()

    Console.ReadKey() |> ignore

    0