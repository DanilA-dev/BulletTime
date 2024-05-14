using Types;

namespace Systems.Intefaces
{
    public interface IPlayerGetService
    {
        public PlayerEntity Create();
        public void Reset();
    }
}