using UnityEngine;
using System.Collections;

[System.Serializable]
public class Message
{
    public object Data
    {
        get; private set;
    }

    public static Message None
    {
        get
        {
            return new Message();
        }
    }

    public Message()
    {
        Data = null;
    }

    public Message(object data)
    {
        Data = data;
    }
}
