using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawn_points;
    public GameObject current_spawn;
    public GameObject[] monsters;
    public static bool spawn_allowed;
    private int spawn_point;
    private int monster;

    void Start()
    {
        spawn_allowed = true;
        InvokeRepeating("Spawn", 0.0f, 10.0f);
    }

    void Spawn()
    {
        if (spawn_allowed)
        {
            spawn_point = Random.Range(0, spawn_points.Length);
            monster = Random.Range(0, monsters.Length);
            current_spawn = Instantiate(monsters[monster], spawn_points[spawn_point].position, Quaternion.identity);
            current_spawn.layer = LayerMask.NameToLayer("Actor");
        }
    }
}
