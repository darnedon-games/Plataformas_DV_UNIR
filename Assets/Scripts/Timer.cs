using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tiempoJuego;
    [SerializeField] private Player player;
    [SerializeField] private Boss boss;
    
    private float tiempoTranscurrido;
    private int minutos;
    private int segundos;
    private bool isRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            tiempoTranscurrido += Time.deltaTime;
            minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
            segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);

            tiempoJuego.text = string.Format("Time: {0:00}:{1:00}", minutos, segundos);
        }
        if ((player == null)||(boss == null))
        {
            isRunning = false;
        }
    }
}
