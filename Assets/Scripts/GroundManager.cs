using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject[] groundPrefabs;
    public GameObject woolPrefab;
    public Transform playerTransform;

    private GameObject[] gos;
    private float spawnZ = 0.0f;
    private float tileLength = 10f;
    private int amGroundOnScreen = 7;
    private int changeTiles = 0;
    private int toReplace = 0;


    public Color blue;
    public Color red;
    public Color green;


    // Start is called before the first frame update
    private void Start()
    {
        gos = new GameObject[amGroundOnScreen];
        for (int i = 0; i < amGroundOnScreen; i++)
        {
            spwnGround();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform.position.y < -10)
        {
            startOver(0);
        }
        if (playerTransform.position.z > 900f)
        {
            startOver(0);
        }

        if (playerTransform.position.z - 10 > (spawnZ - amGroundOnScreen * tileLength))
        {
            createTile();
        }
    }

    private void createTile()
    {
        bool createWool = (changeTiles % 8 == 0);
        bool moveWave = (changeTiles % 20 == 0);
        
        if (changeTiles < 30)
        {
            spwnGround(changeTiles%3, createWool, moveWave);
            changeTiles++;
        }
        else
        {
            changeTiles = 0;
        }
    }

    private void startOver(int prefabIndex = -1)
    {
        spawnZ = 0;
        for (int i = 0; i < amGroundOnScreen; i++)
        {
            spwnGround(prefabIndex);
        }
        playerTransform.position = Vector3.zero;
    }

    private void spwnGround(int prerabIndex = -1, bool createWool = false,bool moveWave = false)
    {
        createGO(prerabIndex);

        //createRandomWool(createWool, prerabIndex);

        
        spawnZ += tileLength ;
    }

    private void createGO(int prerabIndex)
    {
        GameObject go;
        if (prerabIndex == -1)
        {
            go = Instantiate(groundPrefabs[0]) as GameObject;
        }
        else
        {
            go = Instantiate(groundPrefabs[prerabIndex]) as GameObject;
            Destroy(gos[toReplace]);
        }

        gos[toReplace] = go;
        toReplace = (toReplace + 1) % amGroundOnScreen;
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y,spawnZ);
        if(prerabIndex % 3 == 0)
            go.GetComponent<MeshRenderer>().material.color = red;        
        else if(prerabIndex % 3 == 1)
            go.GetComponent<MeshRenderer>().material.color = blue;        
        else
            go.GetComponent<MeshRenderer>().material.color = green;        

        if (prerabIndex == 2)
        {
            go.transform.position += Vector3.left;
        }
    }

    

    // private void createRandomWool(bool createWool, int prerabIndex)
    // {
    //     if (createWool)
    //     {
    //         if (!(woolGO == null))
    //         {
    //             Destroy(woolGO);
    //         }
    //         woolGO = Instantiate(woolPrefab) as GameObject;
    //         woolGO.transform.SetParent(transform);
    //         woolGO.transform.position = Vector3.forward * spawnZ;
    //         woolGO.transform.position += Vector3.up * 0.2f;
    //
    //         if (prerabIndex == 2)
    //         {
    //             woolGO.transform.position += Vector3.left;
    //         }
    //     }
    // }
}
