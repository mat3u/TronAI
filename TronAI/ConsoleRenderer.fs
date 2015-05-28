module ConsoleRenderer

open System
open Engine

let render (w, h) worlds =
    let colors = [ConsoleColor.Red; ConsoleColor.Blue; ConsoleColor.Cyan; ConsoleColor.Yellow; ConsoleColor.Green; ConsoleColor.Magenta]

    let renderHead (player, (x, y)) =
        let org = Console.ForegroundColor

        Console.ForegroundColor <- colors.[player % colors.Length]

        Console.SetCursorPosition(x+1, y+1)
        Console.Write(player)

        Console.ForegroundColor <- org

        System.Threading.Thread.Sleep(10)

    let renderTurn (w, h) world =
        world.Heads |> Seq.iter renderHead

    let initial = List.head worlds
    initial.Taken |> Seq.iter (fun (x, y) ->
        Console.SetCursorPosition(x+1, y+1)
        Console.Write("#")
    )

    worlds |> Seq.iter (renderTurn (w,h))