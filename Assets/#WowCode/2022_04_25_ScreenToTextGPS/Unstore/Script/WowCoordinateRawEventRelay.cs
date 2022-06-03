using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WowCoordinateRawEventRelay : MonoBehaviour
{

    public WowCoordinateRawEvent m_relay;

    public void PushIn(WowCoordinateRaw coordinate) {
        m_relay.Invoke(coordinate);
    }
}
