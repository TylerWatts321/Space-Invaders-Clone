using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    private Vector3 startPos;
    public UFO ufo;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn",30.0f, 30.0f);
    }

    void Spawn()
    {
        Instantiate(ufo.gameObject, transform.position, transform.rotation);
        transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
    }
}
