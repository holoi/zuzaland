using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Unity.Netcode;

public class Firework : MonoBehaviour
{
    //[SerializeField] float m_Speed = 1.6f;

    AudioSource _AudioSource;

    [SerializeField] AudioClip[] _ShootAudios = new AudioClip[3];
    [SerializeField] AudioClip[] _ExplodeAudios = new AudioClip[3];

    [SerializeField] float m_Lifetime = 8f;
    [SerializeField] float m_Lifetime_Vfx = 4f;

    private float m_Duration;

    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();

        var lifetime  = Random.Range(m_Lifetime_Vfx, m_Lifetime_Vfx+2f);

        GetComponent<VisualEffect>().SetFloat("Lifetime", lifetime);

        GetComponent<VisualEffect>().SendEvent("OnPlay");


        // play shoot audio
        var n = Random.Range(0f, _ShootAudios.Length - 1f);
        int m = Mathf.RoundToInt(n);
        _AudioSource.clip = _ShootAudios[m];
        _AudioSource.Play();

        StartCoroutine(WaitAndPlayAudio(lifetime));

    }

    IEnumerator WaitAndPlayAudio(float t)
    {
        yield return new WaitForSeconds(t);
        _AudioSource.Stop();
        var n = Random.Range(0f, _ShootAudios.Length - 1f);
        int m = Mathf.RoundToInt(n);
        _AudioSource.clip = _ExplodeAudios[m];
        _AudioSource.time = 0; // re start at first;
        _AudioSource.Play();
    }


    private void Update()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            //transform.position += m_Speed * Time.deltaTime * transform.forward;
            m_Duration += Time.deltaTime;
            if (m_Duration > m_Lifetime)
            {
                Destroy(gameObject);
            }
        }  
    }

}
