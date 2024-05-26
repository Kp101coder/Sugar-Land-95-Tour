using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dhyan Vyas

public class CubePromptClick : MonoBehaviour
{

    [SerializeField] private TextAsset infoFile;
    [SerializeField] private int index;
    private GameObject canvas;
    private InfoUIToggle script;
    private const string blockTag = Tags.ExtraInfoBlock;
    private string[] infoArr;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("ExtraInfoCanvas");
        script = canvas.GetComponent<InfoUIToggle>();
        infoArr = infoFile.text.Split(new string[] { Environment.NewLine + Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries);

    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.CompareTag(blockTag)) // Check if the hit object has the specified tag
                {   
                    int index = hit.collider.GetComponent<CubePromptClick>().index;
                    script.OpenCard(infoArr[index]);
                }
            }
        }
    }
}
