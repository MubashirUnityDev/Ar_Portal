using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDetection : MonoBehaviour
{
    bool isDoor;
    bool isPassage;
    public GameObject Lense;
    AudioSource m_AudioSource;
    PortalHandler m_PortalHandler;

    private void Start()
    {
        m_PortalHandler = FindAnyObjectByType<PortalHandler>();
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("zone"))
        {
            isDoor = true;
            Debug.Log("IsDoor is true Now");

        }
        if(!isDoor)
        {
            return;
        }
        if(other.CompareTag("Passage")&&isDoor)
        {
            Lense.SetActive(true);
            m_PortalHandler.SetPosters(true);

            Debug.Log("Lense is Active");
            
            m_AudioSource.Play();

        }
    }

    

    
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Passage"))
        {
            Lense.SetActive(false);
            m_PortalHandler.SetPosters(false);
            Debug.Log("Trigger is Exit");
            
            isDoor = false;
        }

        
    }
    private void Update()
    {
        if(m_PortalHandler==null)
        {
            m_PortalHandler = FindAnyObjectByType<PortalHandler>();
        }
    }
}
