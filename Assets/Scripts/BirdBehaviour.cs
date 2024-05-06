using UnityEngine;

public class BirdBehaviour : MonoBehaviour {

    public int flyForce;
    
    private Rigidbody2D body;
    private Animator animator;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (!GameManager.Instance.IsGamePaused() && Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("Fly");
            body.velocityY = 0;
            body.AddForceY(flyForce);
        }

        body.rotation = body.velocityY * 5;
    }
}
