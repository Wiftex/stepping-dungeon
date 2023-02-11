using System;
using Scripts.Stats;
using UnityEngine;

namespace Scripts.Characters
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int damage;
        
        public Stat Health { get; private set; }
        public Stat Damage { get; private set; }

        public void TakeDamage(int value)
        {
            Health.Value -= value;
        }
        
        protected virtual void Awake()
        {
            Health = new Stat(maxHealth, maxHealth);
            Damage = new Stat(damage, damage);
            
            Health.ValueChanged += HealthOnValueChanged;
        }

        private void OnDisable()
        {
            Health.ValueChanged -= HealthOnValueChanged;
        }

        private void HealthOnValueChanged(int value)
        {
            Debug.Log($"Enemy {gameObject.name} health: " + value);

            if(value == 0)
                Destroy(this.gameObject);
        }
    }
}