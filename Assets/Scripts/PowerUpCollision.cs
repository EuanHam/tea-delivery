using UnityEngine;

public class PowerUpCollision : MonoBehaviour
{
    public PowerUpManager powerUpManager;
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player") 
        {
            int result = Random.Range(0,3);

            switch(result)
            {
                // Become Invulnerable
                case 0: 
                    powerUpManager.setInvunerable();
                    break;
                // Extend Total Time
                case 1:
                    powerUpManager.extendTime();
                    break;
                // 2x Money
                case 2:
                    powerUpManager.setDoubleMoney();
                    break;
                default:
                    return;

            }

            Destroy(gameObject);
        }
    }
}
