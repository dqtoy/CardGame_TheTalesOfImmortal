using UnityEngine;
using System.Collections;

public class ReadTxt 
{
    public static string[][] ReadText (string txtName) 
    {
        string[][] textArray;
        TextAsset binAsset = Resources.Load (txtName, typeof(TextAsset)) as TextAsset;
        string[] lineArray = binAsset.text.Split ("\r" [0]);//split the txt by return("/r"[0]);

        textArray = new string[lineArray.Length][];

        for (int i=0; i<lineArray.Length; i++)   {
            textArray[i] = lineArray[i].Split(','); //split the line by ','
        }

        return textArray;

    }

    public static string GetDataByRowAndCol(string[][] textArray, int nRow,int nCol)
    {
        if (textArray.Length <= 0 || nRow >= textArray.Length)
            return "";
        if (nCol >= textArray [0].Length)
            return "";

        return textArray [nRow] [nCol];
    }
        

}
