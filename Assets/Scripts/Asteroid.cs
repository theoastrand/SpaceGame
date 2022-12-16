using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] int points = 5;
    [SerializeField] float maxLifeTime = 6;

    
    void Start()
    {
        // máximos segundos de vida para los asteroides
        Destroy(gameObject, maxLifeTime);
        GetComponent<Rigidbody>().AddForce(new Vector3(0,-speed*100,0));

    }
    
}
