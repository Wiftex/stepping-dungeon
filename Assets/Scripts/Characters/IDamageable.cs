using Scripts.Stats;

namespace Scripts.Characters
{
    public interface IDamageable
    {
        public Stat Health { get; }

        public void TakeDamage(int value);
    }
}