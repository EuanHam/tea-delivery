using UnityEngine;

public class PowerUpCollision : MonoBehaviour
{
    public PowerUpManager powerUpManager;
    [SerializeField] private AudioClip moreTime, shield, coins, ding;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        float newY = startPos.y + Mathf.Cos(Time.time * 2f) * 0.5f;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
        transform.Rotate(Vector3.forward * 90f * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player") 
        {
            int result = Random.Range(0,3);
            switch(result)
            {
                // Become Invulnerable
                case 0: 
                    playShield();
                    powerUpManager.setInvunerable();
                    break;
                // Extend Total Time
                case 1:
                    powerUpManager.extendTime();
                    playMoreTime();
                    break;
                // 2x Money
                case 2:
                    playCoins();
                    powerUpManager.setDoubleMoney();
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
