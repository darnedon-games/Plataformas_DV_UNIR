using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Wizard : MonoBehaviour
{
    [SerializeField] private BolaFuego bolaFuegoPrefab;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaques;
    [SerializeField] private float danhoAtaque;

    private ObjectPool<BolaFuego> bolaFuegoPool;

    private Animator anim;

    private void Awake()
    {
        bolaFuegoPool = new ObjectPool<BolaFuego>(CrearBolaFuego, CogerBolaFuego, DejarBolaFuego);
    }

    private BolaFuego CrearBolaFuego()
    {
        BolaFuego copiaBolaFuego = Instantiate(bolaFuegoPrefab);
        copiaBolaFuego.PoolBolaFuego = bolaFuegoPool;
        return copiaBolaFuego;
    }

    private void CogerBolaFuego(BolaFuego bolaFuego)
    {
        bolaFuego.gameObject.SetActive(true);
    }

    private void DejarBolaFuego(BolaFuego bolaFuego)
    {
        bolaFuego.gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RutinaAtaque());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RutinaAtaque()
    {
        while (true)
        {
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(tiempoAtaques);
        }
    }

    private void LanzarBola()
    {
        BolaFuego copia = bolaFuegoPool.Get();
        copia.transform.position = puntoSpawn.position;
        //copia.transform.rotation = transform.rotation;
        //Instantiate(copia, puntoSpawn.position, transform.rotation);
    }
}
