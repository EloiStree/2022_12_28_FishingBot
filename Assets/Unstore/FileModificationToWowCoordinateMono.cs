using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileModificationToWowCoordinateMono : MonoBehaviour
{
    public WowCoordinateRawEvent m_wowCoordinateFound;
    public string m_lastText;
    public string m_lastFileSource;

    public void PushFileEvent(ObservedFileEvent fileEvent) {
        WowCoordinateRaw coordinate = new WowCoordinateRaw();

        fileEvent.GetFileReference(out Eloi.IMetaAbsolutePathFileGet file);
        m_lastFileSource = file.GetPath();
        if (!Eloi.E_FileAndFolderUtility.Exists(file)) {
            m_lastText = "";
            return;
        }
        WowCoordinateRawParser.LoadFromJsonFile(file, out coordinate);
        m_wowCoordinateFound.Invoke(coordinate);
    }
}
