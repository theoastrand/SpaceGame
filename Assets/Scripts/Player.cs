using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float thrustForce = 5f;
    [SerializeField] float rotationSpeed = 120f;

    
    public static int SCORE = 0;
    public static float xBorderLimit, yBorderLimit;
    private int specials = 10;
    public static float dashSpeed = 50;


    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject gun;

    Rigidbody _rigidbody;

    Vector2 thrustDirection;

    void Start()
    {
        // rigidbody nos permite aplicar fuerzas en el jugador
        _rigidbody = GetComponent<Rigidbody>();

        yBorderLimit = Camera.main.orthographicSize+1;
        xBorderLimit = (Camera.main.orthographicSize+1) * Screen.width / Screen.height;
    }

    private void FixedUpdate()
    {
        // obtenemos las pulsaciones de teclado
        float rotation = Input.GetAxis("Rotate") * rotationSpeed * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * thrustForce;
        // la dirección de empuje por defecto es .right (el eje X positivo)
        thrustDirection = transform.right;

        // rotamos con el eje "Rotate" negativo para que la dirección sea correcta
        transform.Rotate(Vector3.forward, -rotation);

        // añadimos la fuerza capturada arriba a la nave del jugador
        _rigidbody.AddForce(thrust * thrustDirection);
    }

    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit+1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit-1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit+1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit-1;
        transform.position = newPos;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // al pulsar espacio, instanciamos una bala.
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            // cargamos el script Bullet del GO bullet en la variable balaScript            
            Bullet balaScript = bullet.GetComponent<Bullet>();

            // le damos dirección a la bala.
            balaScript.targetVector = transform.right;
        }

        if (Input.GetKeyDown(KeyCode.Return) && specials > 0)
        {
            // al pulsar espacio, instanciamos una bala.
            GameObject bullet1 = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            // cargamos el script Bullet del GO bullet en la variable balaScript            
            Bullet balaScript1 = bullet1.GetComponent<Bullet>();

            // le damos dirección a la bala.
            balaScript1.targetVector = transform.right;

            

            // al pulsar espacio, instanciamos una bala.
            GameObject bullet3 = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            // cargamos el script Bullet del GO bullet en la variable balaScript            
            Bullet balaScript3 = bullet3.GetComponent<Bullet>();

            // le damos dirección a la bala.
            balaScript3.targetVector = transform.up;

            // al pulsar espacio, instanciamos una bala.
            GameObject bullet4 = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            // cargamos el script Bullet del GO bullet en la variable balaScript            
            Bullet balaScript4 = bullet4.GetComponent<Bullet>();

            // le damos dirección a la bala.
            balaScript4.targetVector = -transform.up;

            specials--;
        }
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        // si el jugador se choca con un asteroide significa que hemos muerto
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            Application.LoadLevel(Application.loadedLevel);
            SCORE = 0;
        }
    }

}
