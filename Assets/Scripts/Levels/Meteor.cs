using UnityEngine;

// This manages newly spawned meteor

public class Meteor : MonoBehaviour {
    public float Speed;
    public float HorizontalSpeedVariation;
    public float StartingPositionSpread;

    private void Start()
    {
        // Set random position
        float Pos = Random.Range(StartingPositionSpread * -1, StartingPositionSpread);
        transform.Translate(Vector2.left * Pos);

        // Set random velocity
        float speedH = Random.Range(HorizontalSpeedVariation * -1, HorizontalSpeedVariation);
        Vector2 direction = new Vector2(speedH, Speed);
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction);
    }
}
