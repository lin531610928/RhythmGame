using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + height); 
    }

    // Update is called once per frame
    void Update()
    {
        float interpolation = 10.0f * Time.deltaTime;
        float x = Mathf.Lerp(transform.position.x, player.transform.position.x, interpolation);
        float y = Mathf.Lerp(transform.position.y, player.transform.position.y, interpolation);
        float z = Mathf.Lerp(transform.position.z, player.transform.position.z + height, 10.0f * Time.deltaTime);
        transform.position = new Vector3(x, y ,z);
    }
}
