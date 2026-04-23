using UnityEngine;
using System.Collections;

public class VehicleCollision : MonoBehaviour
{
    [SerializeField] private RobbiController robbiController;

    [SerializeField] private PowerUpManager powerUpManager;
    [SerializeField] private BobaDriver bd;
    [SerializeField] private float stunDuration;

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Vehicle") 
        {
            Debug.Log("vehicle collision!");
            if (!powerUpManager.isInvunerable() && !robbiController.stunned) {
                bd.balance -= 100;
                bd.balance = Mathf.Max(0, bd.balance);
                bd.npcsHit += 1;
                StartCoroutine(stunnedMovement());
            }
        }
    }

    private IEnumerator stunnedMovement()
    {
        robbiController.lockMovement();
        yield return new WaitForSeconds(stunDuration);
        robbiController.unlockMovement();
    }

    public bool isStunned ()
    {
        return robbiController.stunned;
    }
}
