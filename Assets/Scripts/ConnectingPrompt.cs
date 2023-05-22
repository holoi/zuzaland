using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConnectingPrompt : MonoBehaviour
{
    [SerializeField] TMP_Text m_ConnectionText;

    private void OnEnable()
    {
        StartCoroutine(UpdateConnectionText());
    }

    private IEnumerator UpdateConnectionText()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            if (m_ConnectionText.text.Equals("Connecting..."))
            {
                m_ConnectionText.text = "Connecting";
            }
            else if (m_ConnectionText.text.Equals("Connecting"))
            {
                m_ConnectionText.text = "Connecting.";
            }
            else if (m_ConnectionText.text.Equals("Connecting."))
            {
                m_ConnectionText.text = "Connecting..";
            }
            else if (m_ConnectionText.text.Equals("Connecting.."))
            {
                m_ConnectionText.text = "Connecting...";
            }
        }
    }
}

