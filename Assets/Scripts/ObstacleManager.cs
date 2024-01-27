using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    PlayerManager playerManager;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("blue"))
        {
            Destroy(collision.gameObject);
            #region karakter text güncellemesi
            playerManager = collision.transform.parent.GetComponent<PlayerManager>();
            playerManager.stickmanList.Remove(collision.gameObject);
            playerManager.TextUpdate();
            #endregion
            StartCoroutine(Killing());
        }
    }

    IEnumerator Killing()
    {

        yield return new WaitForSeconds(1.2f);
        playerManager.FormatStickMan();
    }
}
