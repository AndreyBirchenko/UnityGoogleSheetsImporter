using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

using UnityEditor.UIElements;

using Task = System.Threading.Tasks.Task;

namespace AB_GoogleSheetImporter.Editor
{
    public class GSImporterEditor : EditorWindow
    {
        private string _savePath;
        private List<TableData> _content;
        private GSImporter _importer;

        [MenuItem("Tools/GoogleSheetsDownloader")]
        public static void ShowExample()
        {
            var wnd = GetWindow<GSImporterEditor>();
            wnd.titleContent = new GUIContent("GoogleSheetsDownloader");
        }

        private void OnDisable()
        {
            SaveData();
        }

        public void CreateGUI()
        {
            _savePath = Application.persistentDataPath + "GSImporterData.json";
            _content = GetContent();
            _importer = new GSImporter();

            var root = rootVisualElement;

            var scrollContainer = new VisualElement();
            scrollContainer.style.flexGrow = 1;

            var scroll = new ScrollView(ScrollViewMode.Vertical);
            scroll.style.flexGrow = 1;
            scroll.style.height = 400;

            scrollContainer.Add(scroll);

            foreach (var tableData in _content)
            {
                CreateDataBox(scroll, tableData);
            }

            root.Add(scrollContainer);

            //Create buttons
            var buttonsContainer = new VisualElement();
            buttonsContainer.style.flexGrow = 0;
            buttonsContainer.style.justifyContent = new StyleEnum<Justify>(Justify.FlexEnd);

            var addButton = CreateButton("Add", buttonsContainer);
            addButton.RegisterCallback<ClickEvent>(x =>
            {
                SaveData();
                CreateDataBox(scroll);
            });

            var downloadButton = CreateButton("Download", buttonsContainer);
            downloadButton.RegisterCallback<ClickEvent>(x => DownloadAsync());

            root.Add(buttonsContainer);
        }

        private Button CreateButton(string text, VisualElement container)
        {
            var button = new Button() {text = text};
            button.style.height = 35;
            button.style.marginTop = 2;
            button.style.marginBottom = 2;
            container.Add(button);
            return button;
        }

        private void CreateDataBox(VisualElement root, TableData tableData = null)
        {
            //Create view
            var box = new Box();
            box.style.marginBottom = 20;
            var selectedToggleField = new Toggle("Selected");
            var downloadUrlField = new TextField("Download url path:");
            var localPathField = new TextField("Local path:");
            var nameField = new TextField("Name:");
            var enumField = new EnumField("File format:", FileFormat.csv);

            if (tableData != null)
            {
                selectedToggleField.value = tableData.Selected;
                downloadUrlField.value = tableData.DownloadUrlPath;
                localPathField.value = tableData.LocalPath;
                nameField.value = tableData.Name;
                enumField.value = tableData.FileFormat;
            }
            else
            {
                selectedToggleField.value = true;
            }

            box.Add(selectedToggleField);
            box.Add(downloadUrlField);
            box.Add(localPathField);
            box.Add(nameField);
            box.Add(enumField);
            root.Add(box);

            //Bind data
            tableData ??= new TableData();
            tableData.Selected = true;
            selectedToggleField.RegisterCallback<ChangeEvent<bool>>(x => tableData.Selected = x.newValue);
            downloadUrlField.RegisterCallback<InputEvent>(x => tableData.DownloadUrlPath = x.newData);
            localPathField.RegisterCallback<InputEvent>(x => tableData.LocalPath = x.newData);
            nameField.RegisterCallback<InputEvent>(x => tableData.Name = x.newData);
            enumField.RegisterValueChangedCallback(x => tableData.FileFormat = (FileFormat) x.newValue);

            if (_content.Contains(tableData) == false)
            {
                _content.Add(tableData);
            }

            var removeButton = new Button(() =>
            {
                root.Remove(box);
                _content.Remove(tableData);
                SaveData();
            });
            removeButton.text = "Remove";
            box.Add(removeButton);
        }

        private List<TableData> GetContent()
        {
            if (File.Exists(_savePath))
            {
                var json = File.ReadAllText(_savePath);
                return JsonConvert.DeserializeObject<List<TableData>>(json);
            }

            return new List<TableData>();
        }

        private void SaveData()
        {
            var json = JsonConvert.SerializeObject(_content);
            File.WriteAllText(_savePath, json);
        }

        private async void DownloadAsync()
        {
            EditorUtility.DisplayProgressBar("GoogleSheetsImporter", "Downloading...", 0);
            try
            {
                for (int i = 0; i < _content.Count; i++)
                {
                    var data = _content[i];
                    if (data.Selected == false) continue;

                    await _importer.DownloadAsync(
                        data.Name,
                        data.DownloadUrlPath,
                        data.LocalPath,
                        data.FileFormat);

                    await Task.Delay(TimeSpan.FromSeconds(0.2f));

                    EditorUtility.DisplayProgressBar("GoogleSheetsImporter", "Downloading...",
                        (float) 1 / _content.Count - i);
                }
            }
            catch (Exception e)
            {
                EditorUtility.ClearProgressBar();
                Debug.LogException(e);
                throw;
            }

            AssetDatabase.Refresh();
            EditorUtility.ClearProgressBar();
        }
    }
}