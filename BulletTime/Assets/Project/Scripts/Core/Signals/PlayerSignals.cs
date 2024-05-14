using Types;

namespace Core.Signals
{
    public class PlayerSignals
    {
        public class ChangeHealth
        {
            public float CurrentHealth { get; private set; }
            public float MaxHealth { get; private set; }
            
            public ChangeHealth(float maxHealth, float currentHealth)
            {
                MaxHealth = maxHealth;
                CurrentHealth = currentHealth;
            }
        }
        
        public class Die {}

        public class Spawn
        {
            public PlayerEntity Player { get; private set; }
            public Spawn(PlayerEntity player)
            {
                Player = player;
            }

            
        }
    }
}