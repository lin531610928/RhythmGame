using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UIView : MonoBehaviour
{
    public GameObject startTips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        startTips.SetActive(false);
        Destroy(startTips);
    }
}
