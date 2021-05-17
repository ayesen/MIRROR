using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

//Credit: some of the script is based on Comp-3 Interactive's Level Generator script
//https://www.youtube.com/watch?v=-6ww4MMi7C0
public class LevelReader : MonoBehaviour
{
    [SerializeField]
    private Decoding[] decodeData; //for each decode data, there will be a pair of symbol related to the prefab
    [SerializeField]
    private TextAsset[] textLevels;//there will be multiple levels
    [SerializeField]
    private Vector2 position = new Vector2(-7.5f, 5.5f);//the position where the level will be spawn

    public static float margin = 1f;

    private float blockCount;
    private int rowCount = 0;
    private int colCount = 0;

    public Text Instruction;
    private Color textColor;

    public List<GameObject> GridBase = new List<GameObject>();

    public int CurrentLevel = 0;

    public bool nextLvl = false;

    void Awake()
    {
        GenLevel(0); //start at level_00
    }

    private void Update()
    {
        
    }

    public void GenLevel(int levelNum)  //function used to generate level
    {

        Vector2 startPosition = position;
        //split the text row by row and read the letter
        string[] rows = Regex.Split(textLevels[levelNum].text, "\r\n|\r|\n");
        foreach (string row in rows)
        {
            foreach (char c in row)
            {
                foreach (Decoding data in decodeData)
                {
                    if (c == data.character)
                    {
                        GameObject gridBlock = Instantiate(data.generatedPrefab, position, Quaternion.identity);
                        gridBlock.transform.SetParent(this.transform);
                        if (gridBlock.tag == "Grid")
                        {
                            GridBase.Add(gridBlock); //if the object generated is a grid object then put it into the grid list
                        }

                    }
                }
                rowCount++;
                position = new Vector2(position.x + margin, position.y);
            }
            colCount++;
            position = new Vector2(startPosition.x, position.y - margin);
        }
        blockCount = rowCount / colCount;
        //set the position of the grid to the middle of the screen
        //this.transform.position = new Vector2(-1 * ((margin * 2) * ((blockCount - 1) / 2)), (margin * 2) * ((blockCount - 1) / 2));


    }

}
