using UnityEngine;

public class MeteorsDrop : MonoBehaviour
{
    public GameObject Meteor;

    public float RespawnTime;
    float Timer = 0f;
	
	void Update ()
    {
        Timer += Time.deltaTime;
        if(Timer >= RespawnTime)
        {
            Timer = 0f;
            Instantiate(Meteor, transform);
        }
	}
}
