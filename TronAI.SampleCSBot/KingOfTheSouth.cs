using System;

namespace TronAI.SampleCSBot
{
    public class KingOfTheSouth : TronAI.SDK.IBot
    {
        public Engine.Direction OnTurn(int player, Tuple<int, int> position, Engine.World world)
        {
            return Engine.Direction.South;
        }
    }
}
