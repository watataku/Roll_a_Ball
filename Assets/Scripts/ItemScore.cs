using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScore : MonoBehaviour
{
    private int basicScore = 10;
    private GameObject shield;

    private float count;
    public float interval;
    private bool isActive;
    
    private bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        shield = transform.GetChild(0).gameObject;
        count = 0f;
        shield.SetActive(isActive);
        StartCoroutine("GameStart");
    }

    // Update is called once per frame
    void Update()
    {
        if(count >= interval)
        {
            count = 0f;
            isActive = !isActive;
            shield.SetActive(isActive);
        }
        
        if(isStart) count += Time.deltaTime;
    }
    
    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3f);
        isStart = true;
    }
}
