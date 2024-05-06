using UnityEngine;

public class PipeCollisionBehaviour : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.CompareTag("Player") || !GameManager.Instance.IsGameRunning()) return;
        
        GameManager.Instance.GameOver();
    }
}
