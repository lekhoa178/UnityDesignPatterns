using System.Collections.Generic;
using UnityEngine;

namespace UnityServiceLocator
{
    public interface ILocalization
    {
        string GetLocalizedWord(string key);
    }

    public class MockLocalization : ILocalization
    {
        readonly List<string> words = new List<string>() { "hund", "katt", "fisk", "bil", "hus" };
        readonly System.Random random = new System.Random();

        public string GetLocalizedWord(string key)
        {
            return words[random.Next(words.Count)];
        }
    }

    public interface ISerializer
    {
        void Serialize();
    }

    public class MockSerializer : ISerializer
    {
        public void Serialize()
        {
            Debug.Log("MockSerializer.Serialize");
        }
    }

    public interface IAudioService
    {
        void Play();
    }

    public class MockAudioService : IAudioService
    {
        public void Play()
        {
            Debug.Log("MockAudioService.Play");
        }
    }

    public interface IGameService
    {
        void StartGame();
    }

    public class MockGameService : IGameService
    {
        public void StartGame()
        {
            Debug.Log("MockGameService.StartGame");
        }
    }

    public class MockMapService : IGameService
    {
        public void StartGame()
        {
            Debug.Log("MockMapService.StartGame");
        }
    }
}