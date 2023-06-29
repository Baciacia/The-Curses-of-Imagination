using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public enum DoorType{
        right,
        left,
        top,
        bot
    }

    public DoorType doorType;
    public float botv = 7.5f, leftv = 6.5f,transPlayerY = 11.46f, transPlayerX = 20.3f;
    private RoomScript room;
    private BossRoom bossroom;
    private ShopRoom shoproom;
    int enemyes;
    private bool canEnter = true; // variabila pentru a verifica dacă jucătorul poate intra

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && canEnter)
        {
            StartCoroutine(EnterAfterDelay(other)); // începe o coroutine pentru a întârzia activarea triggerului
        }
    }

    IEnumerator EnterAfterDelay(Collider2D other)
    {
        room = GetRoomAtPosition(other.transform.position.x, other.transform.position.y);
        
        if(room!= null)
            enemyes = room.nmbEnemyes;
        else enemyes = 0;

        if(enemyes == 0)
        {
            botv = 5.5f;
            leftv = 6.5f;
            transPlayerY = 11.46f;
            transPlayerX = 20.3f;
        }
        else 
        {
            botv = 0f;
            leftv = 0;
            transPlayerY = 0;
            transPlayerX = 0;
        }

        canEnter = false; // setează variabila canEnter la false pentru a preveni activarea triggerului înainte ca intervalul să se încheie
        switch(doorType) 
        {
            case DoorType.bot:
                other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y - botv);
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - transPlayerY, Camera.main.transform.position.z);
                break;
            case DoorType.left:
                other.transform.position = new Vector2(other.transform.position.x - leftv, other.transform.position.y);
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - transPlayerX, Camera.main.transform.position.y, Camera.main.transform.position.z);
                break;
            case DoorType.right:
                other.transform.position = new Vector2(other.transform.position.x + leftv, other.transform.position.y);
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + transPlayerX, Camera.main.transform.position.y, Camera.main.transform.position.z);
                break;
            case DoorType.top:
                other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y + botv);
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + transPlayerY, Camera.main.transform.position.z);
                break;
        }
        CameraManagerScript.currentRoom = GetRoomAtPosition(other.transform.position.x, other.transform.position.y);
        
        yield return new WaitForSeconds(2f); // așteaptă 5 secunde
        canEnter = true; // setează variabila canEnter la true pentru a permite jucătorului să intre din nou
    
    
    }

    private RoomScript GetRoomAtPosition(float x, float y)
    {
        Vector3 position;
        position.x = x;
        position.y = y;
        position.z = 0;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 2f);
        foreach (Collider2D collider in colliders)
        {
            //Debug.Log(collider);
            RoomScript room = collider.gameObject.GetComponent<RoomScript>();

            if (room != null)
            {
                //Debug.Log("salut2 : " + room);
                return room;
            }
        }
        return null;
    }
   
}
