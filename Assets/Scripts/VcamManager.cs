using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VCamManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer transposer;

    //kamera pozisyon deðiþtirme süresi
    float duration = 4f;



    //baþlangýç pozisyonu(yeni seviyeye geçiþte kullanýlacak sadece)
    private Vector3 vCamPos;


    void Start()
    {

        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        GetCameraPos();
        vCamPos = transform.position;
    }


    public void GetCameraPos()              
    {
        Vector3 playerOffset = new Vector3(0, 5.37f, -4.31f);

        if (player.childCount <= 29)
            playerOffset = new Vector3(0, 5.37f, -4.31f);
        else if (player.childCount >= 30 && player.childCount <= 60)
            playerOffset = new Vector3(0, 5.87f, -4.81f);
        else if (player.childCount >= 61 && player.childCount <= 90)
            playerOffset = new Vector3(0, 6.37f, -5.31f);
        else if (player.childCount >= 91 && player.childCount <= 120)
            playerOffset = new Vector3(0, 6.87f, -5.81f);
        else if (player.childCount >= 121 && player.childCount <= 150)
            playerOffset = new Vector3(0, 7.37f, -6.31f);
        else if (player.childCount >= 151 && player.childCount <= 180)
            playerOffset = new Vector3(0, 7.87f, -6.81f);
        else if (player.childCount >= 181)
            playerOffset = new Vector3(0, 8.87f, -7.81f);

        StartCoroutine(ChangeFollowOffset(playerOffset));
    }

    public IEnumerator ChangeFollowOffset(Vector3 targetOffset)
    {
        float elapsedTime = 0f;

        Vector3 startFollowOffset = transposer.m_FollowOffset;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);

            transposer.m_FollowOffset = Vector3.Lerp(startFollowOffset, targetOffset, t);

            yield return null;
        }
    }

    //yeni seviyeye geçtiðinde kamera resetlenir.
    public IEnumerator ResetVCam()
    {
        virtualCamera.Follow = null;
        transposer.m_FollowOffset = vCamPos;
        yield return new WaitForSeconds(2);
        virtualCamera.Follow = player;
        GetCameraPos();
    }



}