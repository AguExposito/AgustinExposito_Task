using UnityEngine;

namespace Code.Player
{
    public class PlayerStats : MonoBehaviour
    {
        public int MaxHealth = 100;
        public int CurrentHealth = 100;

        public void Heal(int amount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
            Debug.Log($"Healed {amount}. HP: {CurrentHealth}/{MaxHealth}");
        }
    }
}
