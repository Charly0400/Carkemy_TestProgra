using System.Collections;
using UnityEngine;

//Gestiona el teleport del jugador al entrar en contacto con un OnTrigger
public class Teleport_Manager : MonoBehaviour
{
    #region Variables

    [Header("REFERENCES")]
    [SerializeField] protected GameManager _gameManager;
    [SerializeField] protected Transform  _transform;
    
    [Header("GENERAL")]
    [SerializeField] protected float delayBeforeTeleport = 0.5f;

    #endregion

    #region Private Methods
    //Detección del personaje cuando este entra en el área del Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportRoutine(other.gameObject));
        }
    }

    //Corrutina para realizar efecto de Fade In / Fade Out
    private IEnumerator TeleportRoutine(GameObject player)
    {
        yield return _gameManager.FadeOut();

        player.transform.position = _transform.position;
        yield return new WaitForSeconds(delayBeforeTeleport);

        yield return _gameManager.FadeIn();
    }
    
    #endregion
}
