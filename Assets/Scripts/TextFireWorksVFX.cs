using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TextFireWorksVFX : MonoBehaviour
{
    public string InputText;

    private VisualEffect _vfx;
    private Vector4 Index1;
    private Vector4 Index2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInputText()
    {
        _vfx = GetComponent<VisualEffect>();

        String2Vector(InputText);

        _vfx.SetVector4("TextIndex1", Index1);
        _vfx.SetVector4("TextIndex2", Index2);
    }

    void String2Vector(string str)
    {
        str = str.ToUpper();

        Debug.Log("upper:" + str);

        if (str.Length < 8)
        {
            for (int i = 0; i < 8; i++)
            {
                if (str.Length < 8)
                {
                    str += '`';
                }
            }
        }
        else if (str.Length == 8)
        {

        }
        else
        {
            str = str.Remove(8);
        }



        Debug.Log("length:" + str);

        //Debug.Log('a' - 65);
        //Debug.Log('A' - 65);


        char[] chars = str.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (i < 4)
            {
                if (chars[i] == 33)
                {
                    Index1[i] = 28; // !
                }
                else if (chars[i] == 44)
                {
                    Index1[i] = 26; // ,
                }
                else if (chars[i] == 46)
                {
                    Index1[i] = 27; // .
                }
                else if (chars[i] == 40)
                {
                    Index1[i] = 29; // (
                }
                else if (chars[i] == 41)
                {
                    Index1[i] = 30; // )
                }
                else
                {
                    Index1[i] = chars[i] - 65;
                }
            }else
            {
                if (chars[i] == 33)
                {
                    Index2[i - 4] = 28; // !
                }
                else if (chars[i] == 44)
                {
                    Index2[i - 4] = 26; // ,
                }
                else if (chars[i] == 46)
                {
                    Index2[i - 4] = 27; // .
                }
                else if (chars[i] == 40)
                {
                    Index2[i - 4] = 29; // (
                }
                else if (chars[i] == 41)
                {
                    Index2[i - 4] = 30; // )
                }
                else
                {
                    Index2[i - 4] = chars[i] - 65;
                }
                
            }
        }

        Debug.Log("1:" + Index1);
        Debug.Log("2:" + Index2);
    }
}
