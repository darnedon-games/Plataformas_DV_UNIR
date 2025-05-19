using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;

    [SerializeField] private Image barraVidaPlayer;

    private SpriteRenderer spriteRenderer;
    private Color colorParpadeo = Color.red;
    private float duracionParpadeo = 0.1f;
    private int numParpadeos = 3;
    private Color colorOriginal;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorOriginal = spriteRenderer.color;
    }

    public void RecibirDanho(float danhoRecibido)
    {
        IniciarParpadeo();

        vidas -= danhoRecibido;

        // Reducimos también la barra de vida del Player de la UI
        if (this.gameObject.CompareTag("PlayerHitBox")){
            barraVidaPlayer.fillAmount = (float)(barraVidaPlayer.fillAmount - (danhoRecibido * 0.01));
        }

        if (vidas <= 0) {
            Destroy(this.gameObject);
        }
    }

    private void IniciarParpadeo()
    {
        StartCoroutine(Parpadeo());
    }

    IEnumerator Parpadeo()
    {
        for (int i = 0; i < numParpadeos; i++)
        {
            spriteRenderer.color = colorParpadeo;
            yield return new WaitForSeconds(duracionParpadeo);
            spriteRenderer.color = colorOriginal;
            yield return new WaitForSeconds(duracionParpadeo);
        }
    }
}
