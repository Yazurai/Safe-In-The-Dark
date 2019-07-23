using UnityEngine;

public class HorizontalPlatform : MonoBehaviour {
    public bool Direction;

    public float MaxX;
    public float MinX;

    public float speed;

    void Update() {
        if (Direction) {
            gameObject.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            Direction = gameObject.transform.position.x < MaxX;
        } else {
            gameObject.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            Direction = gameObject.transform.position.x < MaxX;
        }
    }
}
