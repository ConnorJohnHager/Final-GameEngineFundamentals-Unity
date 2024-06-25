using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_Camera : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        transform.position = player.gameObject.transform.position;
    }
}
