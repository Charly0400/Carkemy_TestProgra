using UnityEngine;
using System.Collections;

public class Teleport_Manager : MonoBehaviour
{
    [SerializeField] protected GameManager _gameManager;
    [SerializeField] protected Transform  _transform;
    [SerializeField] protected float delayBeforeTeleport = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportRoutine(other.gameObject));
        }
    }

    private IEnumerator TeleportRoutine(GameObject player)
    {
        yield return _gameManager.FadeOut();

        player.transform.position = _transform.position;
        yield return new WaitForSeconds(delayBeforeTeleport);

        yield return _gameManager.FadeIn();
    }
}
