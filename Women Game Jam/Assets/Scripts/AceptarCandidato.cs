using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AceptarCandidato : MonoBehaviour
{
    [SerializeField] Image blackScreen;
    [SerializeField] TMP_Text textoFinal;

    [SerializeField] float fadeDuration = 1f;
    [SerializeField] float esperarMenu = 5f;
    [SerializeField] AudioSource audioSource_Fade;

    public void MostrarFinal(string TextoFinal)
    {
        textoFinal.text = TextoFinal;
        audioSource_Fade.Play();
        StartCoroutine(Fade(0f, 1f));
    }

    private IEnumerator Fade(float alphaInicial, float alphaFinal)
    {
        float tiempo = 0f;

        Color screenColor = blackScreen.color;
        screenColor.a = alphaInicial;
        blackScreen.color = screenColor;

        Color textColor = textoFinal.color;
        textColor.a = alphaInicial;
        textoFinal.color = textColor;

        while (tiempo < fadeDuration)
        {
            tiempo += Time.unscaledDeltaTime;

            float alpha = Mathf.Lerp(
                alphaInicial,
                alphaFinal,
                tiempo / fadeDuration
            );

            screenColor.a = alpha;
            textColor.a = alpha;
            blackScreen.color = screenColor;
            textoFinal.color = textColor;

            yield return null;
        }

        screenColor.a = alphaFinal;
        textColor.a = alphaFinal;
        blackScreen.color = screenColor;
        textoFinal.color = textColor;

        yield return new WaitForSeconds(5f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
