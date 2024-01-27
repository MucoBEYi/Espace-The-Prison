using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(2);
        gameObject.SetActive(false);
    }

}
