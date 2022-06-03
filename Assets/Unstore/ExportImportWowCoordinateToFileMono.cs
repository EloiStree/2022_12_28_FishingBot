using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExportImportWowCoordinateToFileMono : MonoBehaviour
{
    public Eloi.AbstractMetaAbsolutePathFileMono m_filePath;

    public void Export(WowCoordinateRaw wowCoordinate) {
        
        try
        {
            WowCoordinateRawParser.SaveInJsonFile(m_filePath,  wowCoordinate);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error happend:" + e.StackTrace);
        }
    }
    public void TryToImport(out WowCoordinateRaw wowCoordinate)
    {
        wowCoordinate = null;
        try {

            WowCoordinateRawParser.LoadFromJsonFile(m_filePath, out wowCoordinate);
        }
        catch(Exception e){
            Debug.LogWarning("Error happend:" + e.StackTrace);
        }
    }
}


public class WowCoordinateRawParser { 

    public static void ConvertToJson(WowCoordinateRaw wowCoordinate, out string json)
    {
        json = JsonUtility.ToJson(wowCoordinate);
    }
    public static void ConvertFromJson(string jsonText, out WowCoordinateRaw wowCoordinate) {

        wowCoordinate = JsonUtility.FromJson<WowCoordinateRaw>(jsonText);
    }

    public static void SaveInJsonFile(Eloi.IMetaAbsolutePathFileGet filePath,  WowCoordinateRaw wowCoordinate) {
        ConvertToJson(wowCoordinate, out string json);
        Eloi.E_FileAndFolderUtility.ExportByOverriding(filePath, json);
    }
    public static void LoadFromJsonFile(Eloi.IMetaAbsolutePathFileGet filePath, out WowCoordinateRaw wowCoordinate) {
        Eloi.E_FileAndFolderUtility.ImportFileAsText(filePath, out string json);
        ConvertFromJson(json, out  wowCoordinate);
    }

}
