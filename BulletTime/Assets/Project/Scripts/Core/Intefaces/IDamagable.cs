public interface IDamagable
{
    public void Damage(float value);
    public bool CanBeDamaged { get; }
}