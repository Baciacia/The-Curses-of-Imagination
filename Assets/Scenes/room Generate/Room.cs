using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width, height , x, y;
    // Start is called before the first frame update
    void Start()
    {
        if(roomGenerator.instance == null)
        {
            return;
        }
        roomGenerator.instance.RegisterRoom(this);
    }
     void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3( x * width, y * height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
