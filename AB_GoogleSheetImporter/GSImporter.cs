using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using UnityEngine;

namespace AB_GoogleSheetImporter.Editor
{
    public class GSImporter
    {
        private static readonly HttpClient _client;
        private readonly Regex _regex;

        public GSImporter()
        {
            _regex = new Regex(@"https://docs\.google\.com/spreadsheets/d/(.+)/");
        }

        static GSImporter()
        {
            _client = new HttpClient();
        }

        public async Task DownloadAsync(string fileName, string sheetUrl, string savePath, FileFormat format)
        {
            ValidateInputData(fileName, sheetUrl, savePath);
            
            var match = _regex.Match(sheetUrl);
            var key = match.Groups[1];
            var stringFormat = GetStringFormat(format);
            var directoryPath = $"{Application.dataPath}/{savePath}";
            var finiteSavePath = $"{directoryPath}/{fileName}.{stringFormat}";

            var downloadUrl = $"https://docs.google.com/spreadsheets/export?id={key}&exportFormat={stringFormat}";

            if (Directory.Exists(directoryPath) == false)
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            try
            {
                var outputData = await _client.GetByteArrayAsync(downloadUrl);
                await using var fileStream = new FileStream(finiteSavePath, FileMode.Create, FileAccess.ReadWrite);
                var binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(outputData);
                binaryWriter.Close();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        private string GetStringFormat(FileFormat format)
        {
            switch (format)
            {
                case FileFormat.csv:
                    return "csv";
                case FileFormat.tsv:
                    return "tsv";
                case FileFormat.ods:
                    return "ods";
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }

        private void ValidateInputData(string fileName, string sheetUrl, string savePath)
        {
            if (string.IsNullOrEmpty(fileName)) throw new Exception("Name can not be null or empty");
            if (string.IsNullOrEmpty(sheetUrl)) throw new Exception("Download url path can not be null or empty");
            if (string.IsNullOrEmpty(savePath)) throw new Exception("Save path can not be null or empty");
        }
    }
}