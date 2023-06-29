using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGeneratorScript : MonoBehaviour
{
    public GameObject roomPrefab,roombossprefab,shopRoomprefab;
    public GameObject EnemyMelee, EnemyRanged, EnemyDuplicated, FlyPrefab, EnemyInv;
    public int minRooms = 6;
    public int maxRooms = 10;
    public int minEnemiesPerRoom = 0;
    public int maxEnemiesPerRoom = 2;

    public GameObject boulderPrefab,tepiPrefab, OilPrefab;


    private List<Vector3> roomPositions = new List<Vector3>();
    private float roomSizeRigthLeft = 20.3f;
    private float roomSizeTopDown = 11.46f;

    void Start()
    {
        int numberOfRooms = UnityEngine.Random.Range(minRooms, maxRooms + 1);

        // Generate the first room at the center of the screen
        Vector3 firstRoomPosition = new Vector3(0, 0, 0);
        roomPositions.Add(firstRoomPosition);
        GameObject firstRoom = Instantiate(roomPrefab, firstRoomPosition, Quaternion.identity, transform);

        Vector3 ShopRoomPosition = new Vector3(0, 11.46f, 0);
        roomPositions.Add(ShopRoomPosition);
        GameObject shopRoom = Instantiate(shopRoomprefab, ShopRoomPosition, Quaternion.identity, transform);

        // Generate the rest of the rooms
        Vector3 previousRoomPosition = firstRoomPosition;
        Vector3 previousPosition = previousRoomPosition;

        for (int i = 0; i < numberOfRooms; i++)
        {
            Vector3 direction;
            float randomValue = UnityEngine.Random.Range(0f, 2f);
            if (randomValue < 0.5f)
            {
                direction = Vector3.right;
            }
            else 
                if(randomValue <1f)
                {
                    direction = Vector3.up;
                }
                else 
                    if(randomValue <1.5f)
                    {
                        direction = Vector3.down;
                    }
                    else
                    {
                        direction = Vector3.left;
                    }
            Vector3 nextRoomPosition;
            bool overlap = false;
            if(direction == Vector3.right || direction == Vector3.left)
            {
                nextRoomPosition = previousRoomPosition + direction * roomSizeRigthLeft;
                foreach (Vector3 existingPosition in roomPositions)
                {
                    if (Vector3.Distance(nextRoomPosition, existingPosition) < roomSizeRigthLeft)
                    {
                        overlap = true;
                        break;
                    }
                } 
            }
            else
            {
                nextRoomPosition = previousRoomPosition + direction * roomSizeTopDown;
                foreach (Vector3 existingPosition in roomPositions)
                {
                    if (Vector3.Distance(nextRoomPosition, existingPosition) < roomSizeTopDown)
                    {
                        overlap = true;
                        break;
                    }
                }
            }
           
           for(int pasi = 0; pasi<4;pasi++)
            {
                if(overlap)
                {
                        if (direction == Vector3.right)
                        {
                            nextRoomPosition = previousRoomPosition + Vector3.up * roomSizeTopDown;
                        }
                        else 
                        {
                            if(direction == Vector3.left)
                            {
                                nextRoomPosition = previousRoomPosition + Vector3.down * roomSizeTopDown;
                            }
                            else 
                            {
                                if(direction == Vector3.down)
                                {
                                    nextRoomPosition = previousRoomPosition + Vector3.right * roomSizeRigthLeft;
                                }
                                else{
                                    nextRoomPosition = previousRoomPosition + Vector3.left * roomSizeRigthLeft;
                                }
                            }
                        }
                        overlap = false;
                        foreach (Vector3 existingPosition in roomPositions)
                        {
                            if(direction == Vector3.right || direction == Vector3.left)
                            {
                                if (Vector3.Distance(nextRoomPosition, existingPosition) < roomSizeRigthLeft)
                                {
                                    overlap = true;
                                    //nextRoomPosition = previousRoomPosition;
                                    break;
                                }

                            }
                            else
                            {
                                if (Vector3.Distance(nextRoomPosition, existingPosition) < roomSizeTopDown)
                                {
                                    overlap = true;
                                    //
                                    break;
                                }
                            }
                        }
                }
            }

            GameObject nextRoom = Instantiate(roomPrefab, nextRoomPosition, Quaternion.identity, transform);

            roomPositions.Add(nextRoomPosition);

            Vector3 roomSize = nextRoom.transform.localScale; 
            
            GenerateEnemies(nextRoomPosition);
            // Generate boulders
            int numberOfBoulders = UnityEngine.Random.Range(3, 5); // Numărul aleatoriu de bolovani între 3 și 4
            for (int j = 0; j < numberOfBoulders; j++)
            {
                Vector3 boulderPosition = GetRandomPositionInRoom(nextRoom.GetComponent<Collider2D>().bounds);

                if (!IsPositionNearDoor(boulderPosition, nextRoom.transform))
                {
                    Instantiate(boulderPrefab, boulderPosition, Quaternion.identity, nextRoom.transform);
                }
            }
            
            int probSpikes = UnityEngine.Random.Range(0,2);
            int probOil = UnityEngine.Random.Range(0,2);
            if(probSpikes <= 1 )
            {
                int numberOfSpikes = UnityEngine.Random.Range(2, 6); // Numărul aleatoriu de bolovani între 3 și 4
                for (int j = 0; j < numberOfSpikes; j++)
                {
                    Vector3 spikePosition = GetRandomPositionInRoom(nextRoom.GetComponent<Collider2D>().bounds);
                    if (!IsPositionNearDoor(spikePosition, nextRoom.transform))
                    {
                        Instantiate(tepiPrefab, spikePosition, Quaternion.identity, nextRoom.transform);
                    }
                }
            }
            if(probOil <= 1 )
            {
                int numberOfOil = UnityEngine.Random.Range(2, 3); 
                for (int j = 0; j < numberOfOil; j++)
                {
                    Vector3 OilPosition = GetRandomPositionInRoom(nextRoom.GetComponent<Collider2D>().bounds);
                    if (!IsPositionNearDoor(OilPosition, nextRoom.transform))
                    {
                        Instantiate(OilPrefab, OilPosition, Quaternion.identity, nextRoom.transform);
                    }
                }
            }
            
            // Update the previous room position
            previousRoomPosition = nextRoomPosition;
            if(i == numberOfRooms - 1)
            {
                overlap = false;
                if(direction == Vector3.right || direction == Vector3.left)
                {
                    nextRoomPosition = previousRoomPosition + direction * roomSizeRigthLeft;
                    foreach (Vector3 existingPosition in roomPositions)
                    {
                        if (Vector3.Distance(nextRoomPosition, existingPosition) < roomSizeRigthLeft)
                        {
                            overlap = true;
                            break;
                        }
                    }
                }
                else
                {
                    nextRoomPosition = previousRoomPosition + direction * roomSizeTopDown;
                    foreach (Vector3 existingPosition in roomPositions)
                    {
                        if (Vector3.Distance(nextRoomPosition, existingPosition) < roomSizeTopDown)
                        {
                            overlap = true;
                            break;
                        }
                    }
                }

                for(int pas = 0; pas <4; pas++)
                {
                    if(overlap)
                    {
                        if (direction == Vector3.right)
                        {
                            nextRoomPosition = previousRoomPosition + Vector3.up * roomSizeRigthLeft;
                        }
                        else 
                        {
                            if(direction == Vector3.left)
                            {
                                nextRoomPosition = previousRoomPosition + Vector3.down * roomSizeRigthLeft;
                            }
                            else 
                            {
                                if(direction == Vector3.down)
                                {
                                    nextRoomPosition = previousRoomPosition + Vector3.right * roomSizeRigthLeft;
                                }
                                else{
                                    nextRoomPosition = previousRoomPosition + Vector3.left * roomSizeRigthLeft;
                                }
                            }
                        }
                        overlap = false;
                        foreach (Vector3 existingPosition in roomPositions)
                        {
                            if(direction == Vector3.right || direction == Vector3.left)
                            {
                                if (Vector3.Distance(nextRoomPosition, existingPosition) < roomSizeRigthLeft)
                                {
                                    overlap = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (Vector3.Distance(nextRoomPosition, existingPosition) < roomSizeTopDown)
                                {
                                    overlap = true;
                                    //
                                    break;
                                }
                            }
                        }
                    }
                }
                nextRoom = Instantiate(roombossprefab, nextRoomPosition, Quaternion.identity, transform);
                Debug.Log("boss!");
                roomPositions.Add(nextRoomPosition);
            } //end if
            
        }
        //END FOR
        
        
        Vector3 currentRoomPosition;
        RoomScript currentRoom;
        ShopRoom currentShop;

        for (int i = 0; i < roomPositions.Count - 1; i++)
        {
            currentRoomPosition = roomPositions[i];
            if(i != 1)
            {
                currentRoom = GetRoomAtPosition(currentRoomPosition);


                if (!HasRoomAtPosition(currentRoomPosition.x ,currentRoomPosition.y + 11.46f,0))
                {
                    currentRoom.doorTop.SetActive(false);
                }
                else{
                    currentRoom.doorTop.SetActive(true);
                }
                
                if (!HasRoomAtPosition(currentRoomPosition.x,currentRoomPosition.y - 11.46f,0))
                {
                    currentRoom.doorBot.SetActive(false);
                }
                else{
                    currentRoom.doorBot.SetActive(true);
                }

                if (!HasRoomAtPosition(currentRoomPosition.x + 20.30f,currentRoomPosition.y,0))
                {
                    currentRoom.doorRight.SetActive(false); 
                }
                else{
                    currentRoom.doorRight.SetActive(true);
                }

                if (!HasRoomAtPosition(currentRoomPosition.x - 20.30f,currentRoomPosition.y,0))
                {
                    currentRoom.doorLeft.SetActive(false);
                }
                else{
                    currentRoom.doorLeft.SetActive(true);
                }
            }
            else
            {
                currentShop = GetShopRoomAtPosition(currentRoomPosition);

                if (!HasRoomAtPosition(currentRoomPosition.x ,currentRoomPosition.y + 11.46f,0))
                {
                    currentShop.doorTop.SetActive(false);
                }
                else
                {
                    currentShop.doorTop.SetActive(true);
                }

                if (!HasRoomAtPosition(currentRoomPosition.x,currentRoomPosition.y - 11.46f,0))
                {
                    currentShop.doorBot.SetActive(false);
                }
                else{
                    currentShop.doorBot.SetActive(true);
                }

                if (!HasRoomAtPosition(currentRoomPosition.x + 20.30f,currentRoomPosition.y,0))
                {
                    currentShop.doorRight.SetActive(false); 
                }
                else{
                    currentShop.doorRight.SetActive(true);
                }

                if (!HasRoomAtPosition(currentRoomPosition.x - 20.30f,currentRoomPosition.y,0))
                {
                    currentShop.doorLeft.SetActive(false);
                }
                else{
                    currentShop.doorLeft.SetActive(true);
                }
            }

        }

        currentRoomPosition = roomPositions[roomPositions.Count - 1];
        Debug.Log(currentRoomPosition);
        
        BossRoom bosscurrentRoom = GetBossRoomAtPosition(currentRoomPosition);
        bosscurrentRoom.isBossAlive = true;

        Debug.Log("Room: " + bosscurrentRoom);
        // Verificați ușa de la nord
        if (!HasRoomAtPosition(currentRoomPosition.x ,currentRoomPosition.y + 11.46f,0))
        {
            bosscurrentRoom.doorTop.SetActive(false);
        }
        else{
            bosscurrentRoom.doorTop.SetActive(true);
        }
        
        // Verificați ușa de la sud
        if (!HasRoomAtPosition(currentRoomPosition.x,currentRoomPosition.y - 11.46f,0))
        {
            bosscurrentRoom.doorBot.SetActive(false);
        }
        else{
            bosscurrentRoom.doorBot.SetActive(true);
        }

        // Verificați ușa de la est
        if (!HasRoomAtPosition(currentRoomPosition.x + 20.30f,currentRoomPosition.y,0))
        {
            bosscurrentRoom.doorRight.SetActive(false); 
        }
        else{
            bosscurrentRoom.doorRight.SetActive(true);
        }

        // Verificați ușa de la vest
        if (!HasRoomAtPosition(currentRoomPosition.x - 20.30f,currentRoomPosition.y,0))
        {
            bosscurrentRoom.doorLeft.SetActive(false);
        }
        else{
            bosscurrentRoom.doorLeft.SetActive(true);
        }

    }

    bool HasRoomAtPosition(float x, float y, float z)
    {
        foreach (Vector3 position in roomPositions)
        {
            if (Mathf.Approximately(position.x, x) && Mathf.Approximately(position.y, y) && Mathf.Approximately(position.z, z))
            {
                return true;
            }
        }
        return false;
    }

    private RoomScript GetRoomAtPosition(Vector3 position)
    {
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

    private BossRoom GetBossRoomAtPosition(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.2f);
        if (colliders.Length > 0)
        {//Debug.Log("boos nu!");
            foreach (Collider2D collider in colliders)
            {
                BossRoom room = collider.gameObject.GetComponent<BossRoom>();
                if (room != null)
                {
                    return room;
                }
            }
        }
        
        return null;
    }

    private ShopRoom GetShopRoomAtPosition(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.2f);
        if (colliders.Length > 0)
        {Debug.Log("shop nu!");
            foreach (Collider2D collider in colliders)
            {
                ShopRoom room = collider.gameObject.GetComponent<ShopRoom>();
                if (room != null)
                {
                    return room;
                }
            }
        }
        
        return null;
    }



    void GenerateEnemies(Vector3 roomPosition)
    {
        RoomScript currRoom = GetRoomAtPosition(roomPosition);
        int numberOfEnemies = UnityEngine.Random.Range(minEnemiesPerRoom, maxEnemiesPerRoom + 1);

        int decision = UnityEngine.Random.Range(0, 3);
        if(decision > 1)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                currRoom.nmbEnemyes = numberOfEnemies;
                Vector3 enemyPosition = roomPosition + new Vector3(UnityEngine.Random.Range(-roomSizeRigthLeft/2f + 1f, roomSizeRigthLeft/2f - 1), UnityEngine.Random.Range(-roomSizeTopDown/2f + 1, roomSizeTopDown/2f -1), 0f);
                int type = UnityEngine.Random.Range(0, 3);
                if(type > 1)
                {
                    GameObject enemy = Instantiate(EnemyMelee, enemyPosition, Quaternion.identity);
                    EnemyController enemyController = enemy.GetComponent<EnemyController>();
                    enemyController.room = currRoom;
                }
                else
                {
                    GameObject enemy = Instantiate(EnemyRanged, enemyPosition, Quaternion.identity);
                    Enemy2Controller enemyController = enemy.GetComponent<Enemy2Controller>();
                    enemyController.room = currRoom;
                }
            }
        }
        else
        {
            int decision2 = UnityEngine.Random.Range(0, 3);
            if(decision2 > 0.5)
            {
                int decision3 = UnityEngine.Random.Range(0, 3);
                if(decision3 > 1)
                {
                    currRoom.nmbEnemyes = 2;
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 enemyPosition = roomPosition + new Vector3(UnityEngine.Random.Range(-roomSizeRigthLeft/2f + 1f, roomSizeRigthLeft/2f - 1), UnityEngine.Random.Range(-roomSizeTopDown/2f + 1, roomSizeTopDown/2f -1), 0f);
                        GameObject enemy = Instantiate(EnemyDuplicated, enemyPosition, Quaternion.identity);

                        EnemyDuplicate enemyController = enemy.GetComponent<EnemyDuplicate>();
                        enemyController.room = currRoom;
                    }
                }
                else
                {
                    currRoom.nmbEnemyes = 3;
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 enemyPosition = roomPosition + new Vector3(UnityEngine.Random.Range(-roomSizeRigthLeft/2f + 1f, roomSizeRigthLeft/2f - 1), UnityEngine.Random.Range(-roomSizeTopDown/2f + 1, roomSizeTopDown/2f -1), 0f);
                        GameObject enemy = Instantiate(EnemyInv, enemyPosition, Quaternion.identity);

                        EnemyInv enemyController = enemy.GetComponent<EnemyInv>();
                        enemyController.room = currRoom;
                    }
                }
            }
            else
            {
                currRoom.nmbEnemyes = 6;
                for (int i = 0; i < 6; i++)
                {
                    Vector3 enemyPosition = roomPosition + new Vector3(UnityEngine.Random.Range(-roomSizeRigthLeft/2f + 1f, roomSizeRigthLeft/2f - 1), UnityEngine.Random.Range(-roomSizeTopDown/2f + 1, roomSizeTopDown/2f -1), 0f);
                    GameObject enemy = Instantiate(FlyPrefab, enemyPosition, Quaternion.identity);

                    Fly enemyController = enemy.GetComponent<Fly>();
                    enemyController.room = currRoom;
                }
            }
        }
    }

    Vector3 GetRandomPositionInRoom(Bounds roomBounds)
    {
        float randomX = UnityEngine.Random.Range(roomBounds.min.x + 1f, roomBounds.max.x-1f);
        float randomY = UnityEngine.Random.Range(roomBounds.min.y + 1.5f, roomBounds.max.y - 1.5f);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);
        return randomPosition;
    }

    bool IsPositionNearDoor(Vector3 position, Transform roomTransform)
    {
        float doorProximityThreshold = 2.5f; // Distanța de proximitate pentru a evita ușile active

        Collider2D[] doorColliders = roomTransform.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D doorCollider in doorColliders)
        {
            if (doorCollider.gameObject.activeSelf && Vector3.Distance(position, doorCollider.transform.position) < doorProximityThreshold)
            {
                return true;
            }
        }

        return false;
    }



}
