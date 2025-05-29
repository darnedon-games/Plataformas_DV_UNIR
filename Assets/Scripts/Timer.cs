using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tiempoJuego;
    private float tiempoTranscurrido;
    private int minutos;
    private int segundos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;
        minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
        segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);

        tiempoJuego.text = string.Format("Time: {0:00}:{1:00}", minutos, segundos);
    }
}
