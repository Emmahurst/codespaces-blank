using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceTravelled : MonoBehaviour
{ 
    private Transform player; 
    [SerializeField] private float score = 0f;
    public TextMeshProUGUI scoreText;
    public Transform startPos;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceTravelled = player.position.x - startPos.position.x;
        score = distanceTravelled * 100;
    }
}
