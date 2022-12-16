using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] GameObject bossPreFab;

    [SerializeField] float spawnRatePerMinute = 30;
    [SerializeField] float spawnRateIncrement = 1;

    private float spawnNext = 0;
    public static Boolean bossSpawned = false;
    private int scoreToBoss = 5;

    void Start()
    {
        //bossPreFab = GameObject.FindGameObjectWithTag("Boss");
        //gun = GameObject.FindGameObjectWithTag("Bullet Spawner");
    }

    void Update() 
    {
        // instanciamos enemigos sólo si ha pasado tiempo suficiente desde el último.
        if (Time.time > spawnNext && Player.SCORE < scoreToBoss)
        {
            // indicamos cuándo podremos volver a instanciar otro enemigo
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            // con cada spawn incrementamos los asteroides por minuto para incrementar la dificultad
            spawnRatePerMinute += spawnRateIncrement;

            // guardamos un punto aleatorio entre las esquinas superiores de la pantalla
            var rand = Random.Range(-Player.xBorderLimit, Player.xBorderLimit);
            var spawnPosition = new Vector2(rand, Player.yBorderLimit);
            
            // instanciamos el asteroide en el punto y con el ángulo aleatorios
            Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        } 
        if (Player.SCORE >= scoreToBoss && !bossSpawned)
        {
            bossSpawned = true;
            //Instantiate(bossPreFab, new Vector2(-Player.xBorderLimit,Player.yBorderLimit), Quaternion.identity);
            Boss.speed = 5;
        } 
    }
}
