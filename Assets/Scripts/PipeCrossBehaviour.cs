using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCrossBehaviour : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.CompareTag("Player")) return;
        
        GameManager.Instance.AddScore();
    }
}
