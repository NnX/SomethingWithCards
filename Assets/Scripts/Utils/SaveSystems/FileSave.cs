using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Utils.SaveSystems
{
    public class FileSave : ISavable
    {
        private SaveDataParameters _saveParameters;
        private readonly string _savePath;
        private bool _isRunningSaveProcess;

        public FileSave()
        {
            _savePath = Application.persistentDataPath + "/save";
            LoadSavedParameters();
        } 
        public SaveDataParameters GetSaveParams()
        {
            return _saveParameters;
        }

        private void LoadSavedParameters()
        {
            FileStream fileStream = new FileStream(_savePath, FileMode.OpenOrCreate);

            try
            {
                if (fileStream.Length == 0)
                {
                    _saveParameters = new SaveDataParameters();
                    // TODO init file save 
                    _saveParameters.deckSize = 0;
                    Debug.Log("Created new save");
                }
                else
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    _saveParameters = (SaveDataParameters)formatter.Deserialize(fileStream);
                    Debug.Log("Loaded hi score");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during Loading = {ex}");
            }
            finally
            {
                fileStream.Close();
            }
        }

        private void SaveParameters()
        {
            if (_isRunningSaveProcess)
            {
                Debug.Log("Saving in progress");
                return;
            }

            _isRunningSaveProcess = true;
            FileStream file = null;

            try
            {
                var formatter = new BinaryFormatter();
                file = File.Create(_savePath);

                if (_saveParameters == null)
                {
                    return;
                }
                
                var newSave = new SaveDataParameters
                {
                    deckSize = _saveParameters.deckSize
                };
                formatter.Serialize(file, newSave);
                
            }
            catch (Exception)
            {
                Debug.LogError("Error during saving");
            }
            finally
            {
                file?.Close();
                _isRunningSaveProcess = false;
                Debug.Log($"Saved data box, path = {_savePath}");
            }
        }

        [Serializable]
        public class SaveDataParameters
        {
            public int deckSize;
        }
 

        public void Save()
        {
            SaveParameters();
        }

        public void Load()
        {
            LoadSavedParameters();
        }
    }
}