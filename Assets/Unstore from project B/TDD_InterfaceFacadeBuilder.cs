using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using WindowNativeInput.Abstract;

public class TDD_InterfaceFacadeBuilder : MonoBehaviour, IPushWindowNativeUtilityFacade
{
    public ProcessesFocusHistoryMono m_frameFocus;
    public WindowProcessRegisterMono m_frameRegister;
    public ProcessesAccessMono m_processAccess;
    public DelegateFrameReference m_currentFocus = new DelegateFrameReference(null);


    private void Awake()
    {
        if (m_frameFocus == null)
            m_frameFocus = ProcessesFocusHistoryInScene.Instance;
        if (m_processAccess == null)
            m_processAccess = ProcessesAccessInScene.Instance;
    }
    public void GetCurrentFrameReference(out IFrameReference frame)
    {
        m_frameFocus.GetCurrentProcessId(out int processid);
        m_currentFocus.SetFrameReference((out int id) => id = processid);
        frame = m_currentFocus;
    }
    public IFrameReference GetCurrentFrameReference( )
    {
        GetCurrentFrameReference(out IFrameReference frame);
        return frame;
    }
    public IFrameReference ConvertToRef(in IntPtrWrapGet pointer)
    {
        pointer.GetAsInt(out int processid);
        m_currentFocus.SetFrameReference((out int id) => id = processid);
        return m_currentFocus;
    }
    public IFrameReference ConvertToRefFromInt( int pointer)
    {
        m_currentFocus.SetFrameReference((out int id) => id = pointer);
        return m_currentFocus;
    }
    public  IntPtrWrapGet ConvertToPtr(in IFrameReference pointer)
    {
        return new IntPtrProcessId(pointer.GetFrameIdAsInt());
    }

    [ContextMenu("HideCurrentFrame")]
    public void HideCurrentFrame()
    {
        HideFrame(GetCurrentFrameReference());

    }
    public void HideFrame(in IFrameReference frame)
    {
        WindowIntPtrUtility.ShowWindow(ConvertToPtr(frame), WindowIntPtrUtility.WindowDisplayType.Hide);
    }
    [ContextMenu("MinimizeCurrentFrame")]
    public void MinimizeCurrentFrame() { MinimizeFrame(GetCurrentFrameReference()); }

    [ContextMenu("MaximizeCurrentFrame")]
    public void MaximizeCurrentFrame() { MaximizeFrame(GetCurrentFrameReference()); }

    [ContextMenu("DisplayCurrentFrame")]
    public void DisplayCurrentFrame() { DisplayFrame(GetCurrentFrameReference()); }

    public void MinimizeFrame(in IFrameReference frame)
    {
        WindowIntPtrUtility.ShowWindow(ConvertToPtr(frame), WindowIntPtrUtility.WindowDisplayType.SmallSize);
    }
    public void MaximizeFrame(in IFrameReference frame)
    {
        WindowIntPtrUtility.ShowWindow(ConvertToPtr(frame), WindowIntPtrUtility.WindowDisplayType.MaxSize);
    }
    public void DisplayFrame(in IFrameReference frame)
    {
        WindowIntPtrUtility.ShowWindow(ConvertToPtr(frame), WindowIntPtrUtility.WindowDisplayType.Normal);
    }

    public void SetFocusOnFrameReference(in IFrameReference frame)
    {
        WindowIntPtrUtility.SetForegroundWindow(ConvertToPtr(frame));
    }



    public Dictionary<string, IFrameReference> m_aliasToFrame = new Dictionary<string, IFrameReference>();
    public void SaveCurrentFrameWithAlias(in string alias) {

        GetCurrentFrameReference(out IFrameReference frame);
        if (!m_aliasToFrame.ContainsKey(alias))
            m_aliasToFrame.Add(alias, frame);
        else m_aliasToFrame[alias] = frame;
    }
    public void GetCurrentFrameFromAlias(in string alias, out IFrameReference frame)
    {
        if (!m_aliasToFrame.ContainsKey(alias))
             frame = null;
        else frame = m_aliasToFrame[alias];
    }

   
    public void FrameKeyStroke(in string keyAlias, in EnumPressType type, in EnumInputMessageType target, in IFrameReference frameTargeted)
    {
        bool isPost = target == EnumInputMessageType.PostInput;

        FindKeyFromAlias(in keyAlias, out bool found, out User32PostMessageKeyEnum key);
        if (found) { 
            if (type == EnumPressType.Press || type == EnumPressType.PressAndRelease)
                SendKeyMessageToWindows.SendKeyDown(key, ConvertToPtr(frameTargeted), in isPost);
            if (type == EnumPressType.Release || type == EnumPressType.PressAndRelease)
                SendKeyMessageToWindows.SendKeyUp(key, ConvertToPtr(frameTargeted), in isPost);
        }
        //SendKeyMessageToWindows.SendKeyDownToProcessChildren(key, m_wowProcess[i].GetAsChildren());
    }

    

    public void DesktopKeyStroke(in string keyAlias, in EnumPressType type)
    {
        FindKeyFromAlias(in keyAlias, out bool found, out User32KeyboardStrokeCodeEnum key);

        if (found)
        {
            if (type == EnumPressType.Press || type == EnumPressType.PressAndRelease)
                SendKeyStrokeToComputerDLL.KeyPress(key);
            if (type == EnumPressType.Release || type == EnumPressType.PressAndRelease)
                SendKeyStrokeToComputerDLL.KeyRelease(key);
        }
    }

    private void FindKeyFromAlias(in string keyAlias, out bool found, out User32KeyboardStrokeCodeEnum key)
    {
        ParseUser32KeyboardStrokeCodeEnum.FindKeyFromAlias(in keyAlias, out  found, out  key);
    }

    private void FindKeyFromAlias(in string keyAlias, out bool found, out User32PostMessageKeyEnum key)
    {
        ParseUser32PostMessageKeyEnum.FindKeyFromAlias(in keyAlias, out  found, out  key);
    }

    public void GetFrameReferenceFromProcessId(in int processid, out IFrameReference frame)
    {
        frame = ConvertToRefFromInt(processid);
    }

    public void GetParentFrameReferenceFromProcessId(in int processid, out IFrameReference frame)
    {
        m_processAccess.GetParentOf(IntPtrProcessId.Int(processid), out bool found, out IntPtrWrapGet pt);
        if (!found)
            frame = ConvertToRefFromInt(0);
        else { frame = ConvertToRef(pt);  }
            
    }

    public void GetDisplayFrameReferenceFromProcessId(in int processid, out IFrameReference frame)
    {
        m_processAccess.GetChildrenThatDisplay(IntPtrProcessId.Int(processid), out bool found, out IntPtrWrapGet pt);
        if (!found)
            frame = ConvertToRefFromInt(0);
        else { frame = ConvertToRef(pt); }
    }


    #region Alias basic access

    public void GetFrameFromAlias(in string alias, out IFrameReference frame)
    {
        throw new NotImplementedException();
    }

    public IFrameReference GetFrameFromAlias(in string alias)
    {
        throw new NotImplementedException();
    }

    public void GetAllAliasProcessesRef(in IFrameReference[] frames)
    {
        throw new NotImplementedException();
    }

    public void GetAllAliasProcessesList(in string[] aliasRegistered)
    {
        throw new NotImplementedException();
    }

    public void GetAllProcessesOfExactName(in IFrameReference[] frames)
    {
        throw new NotImplementedException();
    }

    public void GetFrameSizeInPixel(in IFrameReference frame, out int pixelWidth, out int pixelHeight)
    {
        throw new NotImplementedException();
    }

    public void FrameMouseMove(in IFrameReference frame, in IMouseMove2D moveInformation)
    {
        throw new NotImplementedException();
    }

    public void FrameMouseMove(in IFrameReference frame, in IMouseMove1D moveInformation)
    {
        throw new NotImplementedException();
    }

    public void DesktopMainMouseMove(in IMouseMove2D moveInformation)
    {
        throw new NotImplementedException();
    }

    public void DesktopMainMouseMove(in IMouseMove1D moveInformation)
    {
        throw new NotImplementedException();
    }

    public void FrameMouseClick(in EnumPressType type, in EnumMouseButton buttonType)
    {
        throw new NotImplementedException();
    }

    public void DesktopMouseClick(in EnumPressType type, in EnumMouseButton mouseButton)
    {
        throw new NotImplementedException();
    }

    public void FrameMouseClick(in IFrameReference frame, in EnumPressType type, in EnumMouseButton mouseButton)
    {
        throw new NotImplementedException();
    }

    public void FrameMouseClickAt(in IFrameReference frame, in IMouseMove2DAt coordinate, in EnumPressType type, in EnumMouseButton mouseButton)
    {
        throw new NotImplementedException();
    }
    #endregion
}


namespace WindowNativeInput.Abstract{ 
public interface IPushWindowNativeUtilityFacade {

        #region DOING
        public void FrameKeyStroke(in string keyAlias, in EnumPressType type, in EnumInputMessageType target, in IFrameReference frameTargeted);

        public void DesktopKeyStroke(in string keyAlias, in EnumPressType type);

        #endregion

        #region DONE

        #endregion
        #region TO DO
        public void SaveCurrentFrameWithAlias(in string alias);
        public void GetFrameFromAlias(in string alias, out IFrameReference frame);
        public IFrameReference GetFrameFromAlias(in string alias);

        public void GetAllAliasProcessesRef(in IFrameReference[] frames);
        public void GetAllAliasProcessesList(in string[] aliasRegistered);


        public void HideCurrentFrame();
        public void HideFrame(in IFrameReference frame);
        public void MinimizeCurrentFrame();
        public void MinimizeFrame(in IFrameReference frame);
        public void MaximizeCurrentFrame();
        public void MaximizeFrame(in IFrameReference frame);

        public void GetAllProcessesOfExactName(in IFrameReference[] frames);

        public void DisplayCurrentFrame();
        public void DisplayFrame(in IFrameReference frame);

        public void GetCurrentFrameReference(out IFrameReference frame);
        public void SetFocusOnFrameReference(in IFrameReference frame);

        public void GetFrameReferenceFromProcessId(in int processid, out IFrameReference frame);
        public void GetParentFrameReferenceFromProcessId(in int processid, out IFrameReference frame);
        public void GetDisplayFrameReferenceFromProcessId(in int processid, out IFrameReference frame);
        public  IFrameReference ConvertToRefFromInt(int pointer);


        public void GetFrameSizeInPixel(in IFrameReference frame, out int pixelWidth, out  int pixelHeight);
        public void FrameMouseMove(in IFrameReference frame, in IMouseMove2D moveInformation);
        public void FrameMouseMove(in IFrameReference frame, in IMouseMove1D moveInformation);


        public void DesktopMainMouseMove( in IMouseMove2D moveInformation);
        public void DesktopMainMouseMove( in IMouseMove1D moveInformation);


        public void FrameMouseClick(in EnumPressType type, in EnumMouseButton buttonType);
        public void DesktopMouseClick(in EnumPressType type, in EnumMouseButton mouseButton);
        public void FrameMouseClick(in IFrameReference frame, in EnumPressType type, in EnumMouseButton mouseButton);
        public void FrameMouseClickAt(in IFrameReference frame,in IMouseMove2DAt coordinate, in EnumPressType type, in EnumMouseButton mouseButton);




        #endregion
    }


    public interface IMouseMove2D
    {
        public void GetCoordinateType(out EnumFrameCoordinateType coordinate);
        public void GetMoveType(out EnumMouseMoveType mouseMove);
        public void GetAxisDirection(out EnumRegion2DAxisDirection value);
        public void GetHorizontalValue(out float horizontalValue);
        public void GetVerticalValue(out float verticalValue);
    }

    //public struct DefaultMouseMove2D : IMouseMove2D { }

    public interface IMouseMove2DAt
    {

        public void GetCoordinateType(out EnumFrameCoordinateType coordinate);
        public void GetAxisDirection(out EnumRegion2DAxisDirection value);
        public void GetHorizontalValue(out float horizontalValue);
        public void GetVerticalValue(out float verticalValue);
    }
    //public struct DefaultMouseMove2DAt : IMouseMove2DAt { }


    public interface IMouseMove1D
    {
        public void GetCoordinateType(out EnumFrameCoordinateType coordinate);
        public void GetMoveType(out EnumMouseMoveType mouseMove);
        public void GetDirection(out EnumRegion1DAxisDirection value);
        public void GetValue(out float horizontalValue);
    }
    //public struct DefaultMouseMove1D : IMouseMove1D{ }



    public enum EnumPressType { Press, Release, PressAndRelease }
    public enum EnumInputMessageType { PostInput, SendInput }
    public enum EnumInputTargetType { Frame, OS}


    public enum EnumRegion2DCorner { TopLeft, TopRight, DownLeft, DownRight }
    public enum EnumRegion2DBorder { Left, Top, Down, Right }
    public enum EnumRegion2DAxisDirection { RightTop, RightDown, LeftTop, LeftDown }
    public enum EnumRegion1DAxisDirection { Left2Right, Right2Left, Down2Top, Top2Down }
    public enum EnumMouseMoveType { SetAtPosition, MoveFromPosition }
    public enum EnumMouseButton { Left, Middle, Right }
    public enum EnumFrameCoordinateType { Pixel,Percent }

    public interface IFrameReference
    {
        public int GetFrameIdAsInt();
        public void GetFrameIdAsInt(out int frameId);
    }

    public class DelegateFrameReference : IFrameReference
    {
        public delegate void GetFrameId(out int frameId);
        GetFrameId m_accessid;

        public DelegateFrameReference(GetFrameId accessid)
        {
            m_accessid = accessid;
        }
        public void SetFrameReference(GetFrameId accessid)
        {
            m_accessid = accessid;
        }
        public bool IsDefined() { return m_accessid != null; }

        public int GetFrameIdAsInt()
        {
            if (m_accessid != null) {
                m_accessid(out int id);
                return id;
            }
            return -1;
        }

        public void GetFrameIdAsInt(out int frameId)
        {
            if (m_accessid != null)
            {
                m_accessid(out  frameId);
                return ;
            }
            frameId= - 1;
        }
    }
}