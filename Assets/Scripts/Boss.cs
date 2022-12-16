using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public static float xBorderLimit, yBorderLimit;

    public static float speed = 5;
    private float spawnNext = 0;
    public static int HitPoints = 2;


    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject gun;
    [SerializeField] float spawnRatePerMinute = 30;


    [SerializeField] float thrustForce = 5f;


    Rigidbody _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        yBorderLimit = Camera.main.orthographicSize+1;
        xBorderLimit = (Camera.main.orthographicSize+1) * Screen.width / Screen.height;

        speed = 0;
        HitPoints = 2;
        
    }

    void FixedUpdate()
    {

        transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (Time.time > spawnNext && EnemySpawner.bossSpawned)
        {
             
            spawnNext = Time.time + 15 / spawnRatePerMinute;

            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            // cargamos el script Bullet del GO bullet en la variable balaScript            
            Bullet balaScript = bullet.GetComponent<Bullet>();

            // le damos dirección a la bala.
            balaScript.targetVector = -transform.up;
            
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit+1;
        //else if (newPos.x < -xBorderLimit)
          //  newPos.x = xBorderLimit-1;
        
        transform.position = newPos;

        if (HitPoints == 0)
        {
            Destroy(gameObject);
        }
        
    }
}
