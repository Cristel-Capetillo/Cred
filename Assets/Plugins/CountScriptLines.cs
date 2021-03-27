using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Covid.Slash.Plugins {
#if UNITY_EDITOR
    public class CountScriptLines : EditorWindow {
        [MenuItem("LineCounter/CountLines")]
        public static void LineCounter() {
            var fileNamesByPath = GetFileNamesByPath("Assets");
            var filePathsByFileName = GroupFilePathsByFileName(fileNamesByPath);

            var amountOfLines = 0;
            var amountOfScripts = 0;
            foreach (var paths in filePathsByFileName) {
                foreach (var s in paths.Value) {
                    amountOfLines += File.ReadAllLines(s).Length;
                    amountOfScripts++;
                }
            }
            //var amountOfLines = (from paths in filePathsByFileName from s in paths.Value from line in File.ReadAllLines(s) select s).Count();

            Debug.Log($"Total amount of .cs lines: {amountOfLines}\nAmount of files: {amountOfScripts}");
        }

        static void AddFileNamesByPath(string path, Dictionary<string, string> fileNamesByPath) {
            foreach (var filePath in Directory.GetFiles(path)) {
                if (Path.GetExtension(filePath) != ".cs")
                    continue;
                fileNamesByPath.Add(filePath, Path.GetFileNameWithoutExtension(filePath));
            }

            foreach (var directoryPath in Directory.GetDirectories(path)) {
                AddFileNamesByPath(directoryPath, fileNamesByPath);
            }
        }

        static Dictionary<string, string> GetFileNamesByPath(string path) {
            var result = new Dictionary<string, string>();
            AddFileNamesByPath(path, result);
            return result;
        }

        static Dictionary<string, string[]> GroupFilePathsByFileName(Dictionary<string, string> fileNamesByPath) {
            return fileNamesByPath
                .GroupBy(pair => pair.Value, pair => pair.Key)
                .ToDictionary(
                    group => group.Key,
                    group => group.ToArray());
        }
    }
#endif
}