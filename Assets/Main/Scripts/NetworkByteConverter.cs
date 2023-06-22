using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class NetworkByteConverter
{

    public static byte[] ObjectToByte(this object obj)
    {
        if (obj == null) return null;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream();
        binaryFormatter.Serialize(memoryStream, obj);
        return memoryStream.ToArray();
    }

    public static object ByteToObject(this byte[] bytesData)
    {
        MemoryStream memoryStream = new MemoryStream(bytesData);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        object objectData = binaryFormatter.Deserialize(memoryStream);
        return objectData;
    }

}