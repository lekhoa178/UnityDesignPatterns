using GameCreator.Runtime.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Systems.Persistence
{
    public class FileDataService : IDataService
    {
        ISerializer serializer;
        string dataPath;
        string fileExtension;

        public FileDataService(ISerializer serializer)
        {
            this.serializer = serializer;
            this.dataPath = Application.persistentDataPath;
            this.fileExtension = "json";
        }

        string GetPathToFile(string fileName) => Path.Combine(dataPath, string.Concat(fileName, '.', fileExtension));

        public void Save(GameData data, bool overwrite = true)
        {
            string fileLocation = GetPathToFile(data.Name);

            if (!overwrite && File.Exists(fileLocation))
            {
                throw new IOException($"The file '{data.Name}.{fileExtension}' already exists and cannot be overwritten");
            }

            File.WriteAllText(fileLocation, serializer.Serialize(data));
        }

        public GameData Load(string name)
        {
            string fileLocation = GetPathToFile(name);

            if (!File.Exists(fileLocation))
            {
                throw new ArgumentException($"No persisted GameData with name '{name}'");
            }

            return serializer.Deserialize<GameData>(File.ReadAllText(fileLocation));
        }


        public void Delete(string name)
        {
            string fileLocaltion = GetPathToFile(name);

            if (File.Exists(fileLocaltion))
            {
                File.Delete(fileLocaltion);
            }
        }

        public void DeleteAll()
        {
            foreach (string filePath in Directory.GetFiles(dataPath))
            {
                File.Delete(filePath);
            }
        }

        public IEnumerable<string> ListSaves()
        {
            foreach (string filePath in Directory.GetFiles(dataPath))
            {
                if (Path.GetExtension(filePath) == fileExtension)
                {
                    yield return Path.GetFileNameWithoutExtension(filePath);
                }
            }
        }

    }
}