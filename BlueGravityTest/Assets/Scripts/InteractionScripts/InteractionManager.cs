using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey;
    [SerializeField] private UnityEvent interactionAction;
    
    private bool inRange;

    private void Update()
    {
        if (Input.GetKey(interactionKey) && inRange)
        {
            interactionAction.Invoke();
            inRange = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
