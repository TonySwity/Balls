using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
   public static void Save(ProgressGame progressGame)
   {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/progress.yo";
      FileStream fileStream = new FileStream(path, FileMode.Create);
      ProgressData progressData = new ProgressData(progressGame);
      binaryFormatter.Serialize(fileStream, progressData);
      fileStream.Close();
   }

   public static ProgressData Load()
   {
      string path = Application.persistentDataPath + "/progress.yo";

      if (File.Exists(path))
      {
         BinaryFormatter binaryFormatter = new BinaryFormatter();
         FileStream fileStream = new FileStream(path, FileMode.Open);
         ProgressData progressData = binaryFormatter.Deserialize(fileStream) as ProgressData;
         fileStream.Close();
         return progressData;
      }
      else
      {
         Debug.Log("file not find");
         // ProgressData progressData = new ProgressData(ProgressGame.Instance);
         // return progressData;
         return null;
      }
   }

   public static void Delete()
   {
      string path = Application.persistentDataPath + "/progress.yo";
      File.Delete(path);
   }
}
