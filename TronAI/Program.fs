module TronAI.Main

open System
open TronAI.Engine
open TronAI.ConsoleRenderer
open TronAI.Bots

[<EntryPoint>]
let main argv =
    let randomizer = new System.Random()
    let size = (70, 35)
    let bots = [wriggler; wrigglerL; loony;
                loony; kingOfTheNorth;
                (clrBot @"..\..\..\TronAI.SampleCSBot\bin\Debug\TronAI.SampleCSBot.dll")]
               |> List.mapi (fun i b -> (i, b))

    let history = game randomizer size bots |> Seq.toList

    history |> render size

    Console.ReadKey() |> ignore

    0