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

    private void Start()
    {
        SiguientePregunta();
    }

    public void Continuar()
    {
        preguntasIndex++;
        if(preguntasIndex < preguntasForms.Count)
        {
            SiguientePregunta();
        }
        else
        {
            candidatoFinal.SetActive(true);
            formulario.SetActive(false);
        }
        
    }

    public void SiguientePregunta()
    {
        txtPregunta.text = preguntasForms[preguntasIndex].pregunta;
        txtRespuesta1.text = preguntasForms[preguntasIndex].respuesta1;
        txtRespuesta2.text = preguntasForms[preguntasIndex].respuesta2;
    }
}
