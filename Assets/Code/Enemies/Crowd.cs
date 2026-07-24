using UnityEngine;

public class Crowd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyController ec))
        {
            ec.SlowDown();
            return;
        }

        if (!other.gameObject.CompareTag("Player")) return;
        if (!GameEventManager.instance) return;
        GameEventManager.instance.miscellaneousEvents.OnSlowDown();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyController ec))
        {
            ec.ResetSpeed();
            return;
        }
        
        if (!other.gameObject.CompareTag("Player")) return;
        if (!GameEventManager.instance) return;
        GameEventManager.instance.miscellaneousEvents.OnSpeedUp();
    }
}
