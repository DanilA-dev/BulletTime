using Core.Signals;
using UniRx;
using UnityEngine;

namespace Model
{
    public class Player
    {
        public Vector3 SpawnPosition { get; private set; }
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public float DefaultJumpForce { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public Player(float maxHealth, float defaultJumpForce, Vector3 spawnPosition)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
            DefaultJumpForce = defaultJumpForce;
            SpawnPosition = spawnPosition;
        }

        public void Reset()
        {
            CurrentHealth = MaxHealth;
            MessageBroker.Default.Publish(new PlayerSignals.ChangeHealth(MaxHealth, CurrentHealth));
        }


        public void Damage(float value)
        {
            if (IsDead)
                return;

            CurrentHealth -= value;
            MessageBroker.Default.Publish(new PlayerSignals.ChangeHealth(MaxHealth, CurrentHealth));
            if(CurrentHealth <= 0)
                MessageBroker.Default.Publish(new PlayerSignals.Die());
        }
    }
}