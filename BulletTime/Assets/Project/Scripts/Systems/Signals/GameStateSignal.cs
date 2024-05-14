namespace Systems.Signals
{
    public class GameStateSignal
    {
        public GameStateType State { get; private set; }
        public GameStateSignal(GameStateType state)
        {
            State = state;
        }

    }
}