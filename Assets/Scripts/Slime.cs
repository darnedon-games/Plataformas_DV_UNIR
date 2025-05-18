using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float velocidadPatrulla;
    [SerializeField] private float danhoAtaque;
    private Vector3 destinoActual;
    private int indiceActual = 0;

    private Image barraVidaPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destinoActual = waypoints[indiceActual].position;
        StartCoroutine(Patrulla());

        barraVidaPlayer = GameObject.Find("Canvas/InfoPlayer/Vidas/BarraVida").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Patrulla()
    {
        while (true)
        {
            while (transform.position != destinoActual)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidadPatrulla * Time.deltaTime);
                yield return null;
            }
            DefinirNuevoDestino();
        }
    }

    private void DefinirNuevoDestino()
    {
        indiceActual++;
        if (indiceActual >= waypoints.Length)
        {
            indiceActual = 0;
        }
        destinoActual = waypoints[indiceActual].position;
        EnfocarDestino();
    }

    private void EnfocarDestino()
    {
        if (destinoActual.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            Debug.Log("Player detectado");
        }
        else if (elOtro.gameObject.CompareTag("PlayerHitBox"))
        {
            SistemaVidas sistemaVidasPlayer = elOtro.gameObject.GetComponent<SistemaVidas>();
            sistemaVidasPlayer.RecibirDanho(danhoAtaque);

            // Reducimos también la barra de vida del Player de la UI
            barraVidaPlayer.fillAmount = (float)(barraVidaPlayer.fillAmount - (danhoAtaque * 0.01));
        }
    }
}
