using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Hexed.Modules
{
    internal class MusicHandler
    {
        public static IEnumerator OverrideMusic()
        {
            if (!File.Exists("Hexed\\LoadingMusic.ogg")) yield break;

            Wrappers.Logger.Log("Custom Lobby Music found", Wrappers.Logger.LogsType.Info);

            var audioLoader = UnityWebRequest.Get(string.Format("file://{0}", string.Concat(Directory.GetCurrentDirectory(), "\\Hexed\\LoadingMusic.ogg")).Replace("\\", "/"));
            audioLoader.SendWebRequest();
            while (!audioLoader.isDone) yield return new WaitForEndOfFrame();
            AudioClip myClip = WebRequestWWW.InternalCreateAudioClipUsingDH(audioLoader.downloadHandler, audioLoader.url, false, false, 0);
            MusicManager.Instance.audioSource.clip = myClip;
            MusicManager.Instance.menuMusic = myClip;
            MusicManager.Instance.audioSource.Play();
        }
    }
}
