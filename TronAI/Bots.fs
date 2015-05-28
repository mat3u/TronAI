module Bots

open Engine

let kingOfTheNorth (_ : Player) (_: Position) (_: World) =
    North

let wriggler (me : Player) ((x,y): Position) (world: World) =
    let isEmpty potential =
        world.Taken |> Seq.exists (fun c -> c = potential) |> not

    if isEmpty (x, y-1) then North
    else if isEmpty (x-1, y) then West
    else if isEmpty (x+1, y) then East
    else South

let wrigglerL (_ : Player) ((x,y): Position) (world: World) =
    let isEmpty potential =
        world.Taken |> Seq.exists (fun c -> c = potential) |> not

    if isEmpty (x, y+1) then South
    else if isEmpty (x+1, y) then East
    else if isEmpty (x-1, y) then West
    else North

let loony (_ : Player) ((x,y): Position) (world: World) =
    let r = new System.Random(x * y)

    let isEmpty potential =
        world.Taken |> Seq.exists (fun c -> c = potential) |> not

    let options = seq {
        yield ((x, y+1), South)
        yield ((x, y-1), North)
        yield ((x+1, y), East)
        yield ((x-1, y), West)
    }

    let valid = options
                |> Seq.choose (fun (p, d) ->
                    match isEmpty p with
                    | false -> None
                    | _ -> Some d
                ) |> Seq.toList

    match valid.Length with
    | 0 -> North
    | n -> valid.[r.Next(n)]