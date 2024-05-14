namespace Model
{
    public class EnemyTurret
    {
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public float RotateSpeed { get; private set; }
        public float DetectRadius { get; private set; }
        
        public EnemyTurret(float rotateSpeed, float maxHealth, float detectRadius)
        {
            RotateSpeed = rotateSpeed;
            MaxHealth = maxHealth;
            DetectRadius = detectRadius;
            CurrentHealth = MaxHealth;
        }

        public void Reset() => CurrentHealth = MaxHealth;

    }
}