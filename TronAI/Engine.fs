module Engine

type Direction = | North | East | South | West
type Position = int * int
type Size = int * int

type Player = int
type World = { Taken : Position list; Heads : (Player * Position) list }
type Bot = Player -> Position -> World -> Direction

let skipLast (list: 'a list) =
    list |> Seq.take (list.Length - 1) |> Seq.toList

let moveHead (x,y) direction =
    match direction with
    | North -> (x, y-1)
    | East -> (x+1, y)
    | South -> (x, y+1)
    | West -> (x-1, y)

let randomPosition (r: System.Random) ((w,h): Size) =
    (r.Next(w), r.Next(h))

let initializeGame (r: System.Random) ((w,h): Size) (players: Player seq) =
    let heads = List.ofSeq (seq {
        for p in players do
        yield (p, randomPosition r (w,h))
    })

    let border = List.ofSeq ((seq {
        for x in [-1; w] do
        for y in -1 .. h do
        yield (x,y)

        for x in -1..w do
        for y in [-1;h] do
        yield (x,y)

        yield! heads |> Seq.map snd
    }) |> Seq.distinct)

    {Taken = border; Heads = heads}

let turn (bots: (Player * Bot) seq) (world: World) : World =
    let participants = query {
        for bot in bots do
        join player in world.Heads
            on (fst(bot) = fst(player))
        select (fst(bot), snd(bot), snd(player))
    }

    let potentialMoves = participants
                         |> Seq.toList
                         |> List.map (fun (player, bot, position_n) -> (player, bot player position_n world, position_n))
                         |> List.map (fun (player, direction, position_n) -> (player, moveHead position_n direction))

    let potentialTakens = potentialMoves
                          |> List.scan (fun taken (_, position) -> position :: taken) world.Taken
                          |> skipLast

    let survivors = List.zip potentialMoves potentialTakens
                    |> List.choose (fun ((player, position), taken) ->
                        match (taken |> List.exists (fun c -> c = position)) with
                        | false -> Some (player, position)
                        | _ -> None
                    ) |> Seq.toList

    let taken = world.Taken |> Seq.append (survivors |> Seq.map snd) |> Seq.toList

    {Taken = taken; Heads = survivors}

let game (r: System.Random) (size: Size) (bots: (Player * Bot) seq) =
    let initialWorld = initializeGame r size (bots |> Seq.map fst)

    let turn' = turn (bots |> Seq.toList)

    initialWorld
    |> Seq.unfold (fun world ->
        match world.Heads |> Seq.length with
        | 0 -> None
        | 1 -> None
        | _ ->
            let next = turn' world
            Some (next, next)
        )