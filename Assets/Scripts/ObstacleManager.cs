using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField] ParticleSystem blueParticle;

    private void OnCollisionEnter(Collision collision)
    {
        #region karakterimize çarparsa onu öldürür
        if (collision.collider.CompareTag("blue"))
        {
            Destroy(collision.gameObject);
            //mavi efekt çýkar ölen karakterin üzerinde
            Instantiate(blueParticle, collision.transform.position, Quaternion.identity);

            #region karakter text ve liste güncellemesi
            playerManager = collision.transform.parent.GetComponent<PlayerManager>();
            playerManager.stickmanList.Remove(collision.gameObject);
            playerManager.TextUpdate();
            #endregion
            StartCoroutine(FormatStickman());
        }
        #endregion
    }

    IEnumerator FormatStickman()
    {
        yield return new WaitForSeconds(1.2f);
        playerManager.FormatStickMan();
    }
}
