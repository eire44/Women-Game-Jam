using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FormsController : MonoBehaviour
{
    [SerializeField] List<Preguntas> preguntasForms = new List<Preguntas>();
    int preguntasIndex = 0;

    public TMP_Text txtPregunta;
    public TMP_Text txtRespuesta1;
    public TMP_Text txtRespuesta2;
    public GameObject formulario;
    public GameObject candidatoFinal;
    public TMP_Text descripcionCandidato;
    string[] respuestasElegidas;

    private void Start()
    {
        SiguientePregunta();
    }

    public void Continuar(string opcionElegida)
    {
        respuestasElegidas[preguntasIndex] = opcionElegida;
        preguntasIndex++;
        if(preguntasIndex < preguntasForms.Count)
        {
            //SiguientePregunta();
        }
        else
        {
            candidatoFinal.SetActive(true);
            formulario.SetActive(false);
            descripcionCandidato.text = "Aquí tienes a tu pareja ideal, lee atentamente su perfil y acéptalo como el amor de tu vida!\n" +
                $"Ama a los {respuestasElegidas[0]}\n" +
                $"Prefiere los {respuestasElegidas[1]}\n" +
                $"Quisiera vacacionar siempre en {respuestasElegidas[2]}\n";
        }
        
    }

    public void SiguientePregunta()
    {
        txtPregunta.text = preguntasForms[preguntasIndex].pregunta;
        txtRespuesta1.text = preguntasForms[preguntasIndex].respuesta1;
        txtRespuesta2.text = preguntasForms[preguntasIndex].respuesta2;
    }
}
