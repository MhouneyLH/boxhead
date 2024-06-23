namespace Boxhead.Interfaces
{
    /// <summary>
    /// Represents an object that can take damage. (e. g. the player, an enemy, a destructible object, etc.)
    /// </summary> 
    public interface IDamageable
    {
        void TakeDamage(float damage);
    }
}