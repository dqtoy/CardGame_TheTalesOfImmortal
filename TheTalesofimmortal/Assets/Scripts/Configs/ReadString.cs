using System;


public class ReadString
{
//    public static T GetEnum<T>(string s) where T:Enum{
//        int i=int.Parse(s);
//        return (T)i;
//    }

    public static int[] GetInts(string s){
        int[] id;
        if (s.Contains("|"))
        {
            string[] ss = s.Split('|');
            id = new int[ss.Length];
            for (int i = 0; i < ss.Length; i++)
                id[i] = int.Parse(ss[i]);
        }
        else
        {
            id = new int[]{ int.Parse(s) };
        }
        return id;
    }
}


