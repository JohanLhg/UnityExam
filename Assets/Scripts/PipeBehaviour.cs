using UnityEngine;

public class PipeScript : MonoBehaviour {
    public float speed;

    private Vector3 velocityVector;

    void Start() {
        velocityVector = new Vector3(-speed, 0, 0);
    }

    void Update() {
        if (GameManager.Instance.IsGameRunning()) {
            if (transform.position.x < -3) {
                Destroy(gameObject);
                return;
            }
            transform.position += velocityVector * Time.deltaTime;
        }
    }
}
