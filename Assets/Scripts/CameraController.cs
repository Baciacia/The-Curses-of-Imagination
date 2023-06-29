using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public Transform playerTransform; // referinta catre transformul jucatorului
    //public float followSpeed = 5f; // viteza cu care camera urmareste jucatorul

    // Update is called once per frame
    void Update()
    {
        // urmareste jucatorul cu o anumita intarziere
        //transform.position = Vector3.Lerp(transform.position, playerTransform.position, followSpeed * Time.deltaTime);
        
        // rotirea camerei sa fie in functie de rotirea jucatorului
        //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }
}
