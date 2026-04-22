using UnityEngine;

public class PowerUpCollision : MonoBehaviour
{
    public PowerUpManager powerUpManager;
    [SerializeField] private AudioClip moreTime, shield, coins;
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
                    playShield();
                    break;
                // Extend Total Time
                case 1:
                    powerUpManager.extendTime();
                    playMoreTime();
                    break;
                // 2x Money
                case 2:
                    powerUpManager.setDoubleMoney();
                    playCoins();
                    break;
                default:
                    return;

            }

            Destroy(gameObject);
        }
    }


    public void playMoreTime()
    {
        if (moreTime != null)
        {
            AudioSource.PlayClipAtPoint(moreTime, transform.position);
        }
    }

    public void playShield()
    {
        if (shield != null)
        {
            AudioSource.PlayClipAtPoint(shield, transform.position);
        }
    }

    public void playCoins()
    {
        if (coins != null)
        {
            AudioSource.PlayClipAtPoint(coins, transform.position);
        }
    }
}
