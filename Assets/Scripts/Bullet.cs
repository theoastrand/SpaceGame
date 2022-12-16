using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] int speed = 10;
    [SerializeField] float maxLifeTime = 3;
    public Vector3 targetVector;

    void Start()
    {
        // nada más nacer, le damos unos segundos de vida, lo suficiente para salir de la pantalla
        Destroy(gameObject, maxLifeTime);
    }

    void Update()
    {
        // la bala se mueve en la dirección que tenía el jugador al disparar
        transform.Translate(targetVector * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            IncreaseScore();
        }

        if (other.gameObject.tag == "Player")
        {
            Application.LoadLevel(Application.loadedLevel);
            Player.SCORE = 0;
        }

        if (other.gameObject.tag == "Boss" && EnemySpawner.bossSpawned)
        {
            Boss.HitPoints --;
            Destroy(gameObject);
        }

    }
    
    public void IncreaseScore()
    {
        // cuando un asteroide es destruído, llama a esta función para darnos puntos.
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // llamamos a esta función cada vez que ganamos puntos para actualizar el marcador
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Score: " + Player.SCORE;
    }

}
