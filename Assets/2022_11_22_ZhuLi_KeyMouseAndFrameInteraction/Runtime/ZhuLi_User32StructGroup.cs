using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static partial class ZhuLiStruct
{
    public static partial class User32
    {
        [System.Serializable]
        public struct AxesCoordinateInfo
        {
            public ZhuLiEnum.User32MoveType m_moveType;
            public ZhuLiEnum.User32ValueType m_valueType;
            public ZhuLiEnum.User32AxesDirection2D m_axesDirection;
            public float m_horizontalValue;
            public float m_verticalValue;
        }
        [System.Serializable]
        public struct AxisCoordinateInfo
        {
            public ZhuLiEnum.User32MoveType m_moveType;
            public ZhuLiEnum.User32ValueType m_valueType;
            public ZhuLiEnum.User32AxisDirection2D m_axesDirection;
            public float m_valueToUse;
        }
        [System.Serializable]
        public struct AxesSetCoordinateInfo
        {
            public ZhuLiEnum.User32ValueType m_valueType;
            public ZhuLiEnum.User32AxesDirection2D m_axesDirection;
            public float m_horizontalValue;
            public float m_verticalValue;
        }
        [System.Serializable]
        public struct AxisSetCoordinateInfo
        {
            public ZhuLiEnum.User32ValueType m_valueType;
            public ZhuLiEnum.User32AxisDirection2D m_axesDirection;
            public float m_valueToUse;
        }
        [System.Serializable]

        public struct MouseAction
        {
            public ZhuLiEnum.User32MouseButton m_mouseButtonType;
            public ZhuLiEnum.User32PressType m_pressionType;
        }

        [System.Serializable]
        public struct ProcessUniqueId
        {
            public int m_processId;
        }
        [System.Serializable]
        public struct ProcessesArrayOfId
        {
            public int[] m_processesId;
        }
        [System.Serializable]
        public struct ProcessesExactName
        {
            public string[] m_processesNameId;
        }

    }
}