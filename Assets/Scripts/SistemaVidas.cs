using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;

    [SerializeField] private Image barraVidaPlayer;
    public void RecibirDanho(float danhoRecibido)
    {
        vidas -= danhoRecibido;

        // Reducimos también la barra de vida del Player de la UI
        if (this.gameObject.CompareTag("PlayerHitBox")){
            barraVidaPlayer.fillAmount = (float)(barraVidaPlayer.fillAmount - (danhoRecibido * 0.01));
        }

        if (vidas <= 0) {
            Destroy(this.gameObject);
        }
    }
}
