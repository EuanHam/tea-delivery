using UnityEngine;
using System.Collections;

public class VehicleCollision : MonoBehaviour
{
    [SerializeField] private RobbiController robbiController;

    [SerializeField] private PowerUpManager powerUpManager;
    [SerializeField] private BobaDriver bd;
    [SerializeField] private float stunDuration;
    [SerializeField] private AudioClip coin_drop, dizzy_sound, block;

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Vehicle") 
        {
            if (!powerUpManager.isInvunerable() && !robbiController.stunned) {
                if (bd.balance > 0)
                {
                    PlayCoinDrop();
                }
                bd.balance -= 100;
                bd.balance = Mathf.Max(0, bd.balance);
            
                StartCoroutine(stunnedMovement());
            } else if (powerUpManager.isInvunerable())
            {
                PlayBlock();
            }
        }

        if (c.gameObject.tag == "NPC")
        {
            if (powerUpManager.isInvunerable())
            {
                PlayBlock();
            }
        }
    }
    private IEnumerator stunnedMovement()
    {
        robbiController.lockMovement();
        PlayDizzy();
        yield return new WaitForSeconds(stunDuration);
        robbiController.unlockMovement();
    }

    public bool isStunned ()
    {
        return robbiController.stunned;
    }

    public void PlayCoinDrop()
    {
        if (coin_drop != null)
        {
            AudioSource.PlayClipAtPoint(coin_drop, transform.position);
        }
    }

    public void PlayDizzy()
    {
        if (coin_drop != null)
        {
            AudioSource.PlayClipAtPoint(dizzy_sound, transform.position);
        }
    }
    public void PlayBlock()
    {
        if (block != null)
        {
            AudioSource.PlayClipAtPoint(block, transform.position);
        }
    }
}
