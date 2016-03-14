// NO LONGER USED

using UnityEngine;
using System.Collections;

public class Paintbrush : MonoBehaviour {

    private Renderer rend;
    private Material brushTip;
    private Color paintColor;

	void Start () {
        rend = GetComponent<Renderer>();
        //brushTip = rend.materials[5];
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Comma) || ((Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.Slash))))
        {
            int colorIndex;
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                colorIndex = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Period))
            {
                colorIndex = 1;
            }
            else
            {
                colorIndex = 2;
            }
            brushTip.color = ColorManager.colorManager.ColorArray[colorIndex];
        }

        /* CYCLE FEATURE
        if ((Input.GetKeyDown(KeyCode.Period) || Input.GetMouseButtonDown(0)) || (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Slash)))
        {
            if (Input.GetKeyDown(KeyCode.Period) || Input.GetMouseButtonDown(0))
            {
                // Color 1
                paintColor = ColorPalette.palette.CurrentColors()[0];
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Slash))
            {
                // Color 2
                paintColor = ColorPalette.palette.CurrentColors()[1];
            }
            paint(paintColor);
        }    
        */
    }

    void paint(Color c)
    {
        //brushTip.color = paintColor;
    }
}
