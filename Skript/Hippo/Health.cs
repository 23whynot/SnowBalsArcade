using System;
using UnityEngine;

public class Health : MonoBehaviour
{
   [SerializeField] int health = 3;
   
   public event Action OnHealthChanged;
   public event Action OnDeath;

   public int GetHealth()
   {
      return health;
   }
   
   public void MinusHP()
   {
      health--;
      OnHealthChanged?.Invoke();
      if (health == 0)
      {
         OnDeath?.Invoke();
      }
   }
}
