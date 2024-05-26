using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dhyan Vyas; Edited by Aaditya Puttagunta

public class CubePromptClick : MonoBehaviour
{

    [SerializeField] private string info;
    private GameObject canvas;
    private InfoUIToggle script;
    public const string blockTag = Tags.ExtraInfoBlock;



    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("ExtraInfoCanvas");
        script = canvas.GetComponent<InfoUIToggle>();
        
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.CompareTag(blockTag)) // Check if the hit object has the specified tag
                {
                    string text = hit.collider.GetComponent<CubePromptClick>().info;
                    script.OpenCard(text);
                }

            }
        }
    }
}
