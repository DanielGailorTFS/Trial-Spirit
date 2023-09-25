using UnityEngine;

namespace Ilumisoft.HealthSystem.UI
{
    public class PlayerHealthbar : Healthbar
    {
        public GameObject Player;

        protected virtual void Awake()
        {
            if (Player != null)
            {
                Health = Player.GetComponent<Base_Stats>();
            }
        }
    }
}