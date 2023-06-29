using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerScript : MonoBehaviour
{
    public static RoomScript currentRoom;

    void Start()
    {
        currentRoom = GetRoomAtPosition(transform.position.x, transform.position.y);
    }

    private RoomScript GetRoomAtPosition(float x, float y)
    {
        Vector3 position;
        position.x = x;
        position.y = y;
        position.z = 0;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            RoomScript room = collider.gameObject.GetComponent<RoomScript>();
            if (room != null)
            {
                return room;
            }
        }
        return null;
    }
}
