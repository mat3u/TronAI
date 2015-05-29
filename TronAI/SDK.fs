module TronAI.SDK

open Engine

type IBot =
    abstract OnTurn : Player -> Position -> World -> Direction