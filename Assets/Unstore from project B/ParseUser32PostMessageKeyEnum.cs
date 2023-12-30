using System;

public class ParseUser32PostMessageKeyEnum
{
    public static void FindKeyFromAlias(in string keyAlias, out bool found, out User32PostMessageKeyEnum key)
    {

        #region Char to enum

        found = false;
        key = User32PostMessageKeyEnum.VK_A;
        if (keyAlias == null || keyAlias.Length == 0) { found = false; return; }
        if (keyAlias.Length == 1)
        {
            char c = keyAlias[0];
            switch (c)
            {
                case '↓': found = true; key = User32PostMessageKeyEnum.VK_DOWN; return;
                case '←': found = true; key = User32PostMessageKeyEnum.VK_LEFT; return;
                case '→': found = true; key = User32PostMessageKeyEnum.VK_RIGHT; return;
                case '↑': found = true; key = User32PostMessageKeyEnum.VK_UP; return;
                case ' ': found = true; key = User32PostMessageKeyEnum.VK_SPACE; return;
                case '0': found = true; key = User32PostMessageKeyEnum.VK_0; return;
                case '1': found = true; key = User32PostMessageKeyEnum.VK_1; return;
                case '2': found = true; key = User32PostMessageKeyEnum.VK_2; return;
                case '3': found = true; key = User32PostMessageKeyEnum.VK_3; return;
                case '4': found = true; key = User32PostMessageKeyEnum.VK_4; return;
                case '5': found = true; key = User32PostMessageKeyEnum.VK_5; return;
                case '6': found = true; key = User32PostMessageKeyEnum.VK_6; return;
                case '7': found = true; key = User32PostMessageKeyEnum.VK_7; return;
                case '8': found = true; key = User32PostMessageKeyEnum.VK_8; return;
                case '9': found = true; key = User32PostMessageKeyEnum.VK_9; return;
                case '-': found = true; key = User32PostMessageKeyEnum.VK_OEM_MINUS; return;
                case '+': found = true; key = User32PostMessageKeyEnum.VK_ADD; return;
                case '/': found = true; key = User32PostMessageKeyEnum.VK_DIVIDE; return;
                case '*': found = true; key = User32PostMessageKeyEnum.VK_MULTIPLY; return;
                case '.': found = true; key = User32PostMessageKeyEnum.VK_OEM_COMMA; return;

                case 'a': case 'A': found = true; key = User32PostMessageKeyEnum.VK_A; return;
                case 'b': case 'B': found = true; key = User32PostMessageKeyEnum.VK_B; return;
                case 'c': case 'C': found = true; key = User32PostMessageKeyEnum.VK_C; return;
                case 'd': case 'D': found = true; key = User32PostMessageKeyEnum.VK_D; return;
                case 'e': case 'E': found = true; key = User32PostMessageKeyEnum.VK_E; return;
                case 'f': case 'F': found = true; key = User32PostMessageKeyEnum.VK_F; return;
                case 'g': case 'G': found = true; key = User32PostMessageKeyEnum.VK_G; return;
                case 'h': case 'H': found = true; key = User32PostMessageKeyEnum.VK_H; return;
                case 'i': case 'I': found = true; key = User32PostMessageKeyEnum.VK_I; return;
                case 'j': case 'J': found = true; key = User32PostMessageKeyEnum.VK_J; return;
                case 'k': case 'K': found = true; key = User32PostMessageKeyEnum.VK_K; return;
                case 'l': case 'L': found = true; key = User32PostMessageKeyEnum.VK_L; return;
                case 'm': case 'M': found = true; key = User32PostMessageKeyEnum.VK_M; return;
                case 'n': case 'N': found = true; key = User32PostMessageKeyEnum.VK_N; return;
                case 'o': case 'O': found = true; key = User32PostMessageKeyEnum.VK_O; return;
                case 'p': case 'P': found = true; key = User32PostMessageKeyEnum.VK_P; return;
                case 'q': case 'Q': found = true; key = User32PostMessageKeyEnum.VK_Q; return;
                case 'r': case 'R': found = true; key = User32PostMessageKeyEnum.VK_R; return;
                case 's': case 'S': found = true; key = User32PostMessageKeyEnum.VK_S; return;
                case 't': case 'T': found = true; key = User32PostMessageKeyEnum.VK_T; return;
                case 'u': case 'U': found = true; key = User32PostMessageKeyEnum.VK_U; return;
                case 'v': case 'V': found = true; key = User32PostMessageKeyEnum.VK_V; return;
                case 'w': case 'W': found = true; key = User32PostMessageKeyEnum.VK_W; return;
                case 'x': case 'X': found = true; key = User32PostMessageKeyEnum.VK_X; return;
                case 'y': case 'Y': found = true; key = User32PostMessageKeyEnum.VK_Y; return;
                case 'z': case 'Z': found = true; key = User32PostMessageKeyEnum.VK_Z; return;

                default:
                    break;
            }
        }
        if (keyAlias.Length == 2)
        {
            if ((keyAlias[0] == 'f' || keyAlias[0] == 'F'))
            {
                char c = keyAlias[1];
                switch (c)
                {
                    case '1': found = true; key = User32PostMessageKeyEnum.VK_F1; return;
                    case '2': found = true; key = User32PostMessageKeyEnum.VK_F2; return;
                    case '3': found = true; key = User32PostMessageKeyEnum.VK_F3; return;
                    case '4': found = true; key = User32PostMessageKeyEnum.VK_F4; return;
                    case '5': found = true; key = User32PostMessageKeyEnum.VK_F5; return;
                    case '6': found = true; key = User32PostMessageKeyEnum.VK_F6; return;
                    case '7': found = true; key = User32PostMessageKeyEnum.VK_F7; return;
                    case '8': found = true; key = User32PostMessageKeyEnum.VK_F8; return;
                    case '9': found = true; key = User32PostMessageKeyEnum.VK_F9; return;
                    default:
                        break;
                }
            }
        }
        if (keyAlias.Length == 3)
        {
            if ((keyAlias[0] == 'n' || keyAlias[0] == 'N') && (keyAlias[1] == 'p' || keyAlias[1] == 'P'))
            {
                char c = keyAlias[2];
                switch (c)
                {
                    case '0': found = true; key = User32PostMessageKeyEnum.VK_NUMPAD0; return;
                    case '1': found = true; key = User32PostMessageKeyEnum.VK_NUMPAD1; return;
                    case '2': found = true; key = User32PostMessageKeyEnum.VK_NUMPAD2; return;
                    case '3': found = true; key = User32PostMessageKeyEnum.VK_NUMPAD3; return;
                    case '4': found = true; key = User32PostMessageKeyEnum.VK_NUMPAD4; return;
                    case '5': found = true; key = User32PostMessageKeyEnum.VK_NUMPAD5; return;
                    case '6': found = true; key = User32PostMessageKeyEnum.VK_NUMPAD6; return;
                    case '7': found = true; key = User32PostMessageKeyEnum.VK_NUMPAD7; return;
                    case '8': found = true; key = User32PostMessageKeyEnum.VK_NUMPAD8; return;
                    case '9': found = true; key = User32PostMessageKeyEnum.VK_NUMPAD9; return;
                    case '-': found = true; key = User32PostMessageKeyEnum.VK_OEM_MINUS; return;
                    case '+': found = true; key = User32PostMessageKeyEnum.VK_ADD; return;
                    case '/': found = true; key = User32PostMessageKeyEnum.VK_DIVIDE; return;
                    case '*': found = true; key = User32PostMessageKeyEnum.VK_MULTIPLY; return;
                    case '.': found = true; key = User32PostMessageKeyEnum.VK_OEM_PERIOD; return;
                    default:
                        break;
                }
            }
            if ((keyAlias[0] == 'f' || keyAlias[0] == 'F'))
            {
                char c1 = keyAlias[1];
                if (c1 == '1')
                {
                    switch (keyAlias[2])
                    {
                        case '0': found = true; key = User32PostMessageKeyEnum.VK_F10; return;
                        case '1': found = true; key = User32PostMessageKeyEnum.VK_F11; return;
                        case '2': found = true; key = User32PostMessageKeyEnum.VK_F12; return;
                        case '3': found = true; key = User32PostMessageKeyEnum.VK_F13; return;
                        case '4': found = true; key = User32PostMessageKeyEnum.VK_F14; return;
                        case '5': found = true; key = User32PostMessageKeyEnum.VK_F15; return;
                        case '6': found = true; key = User32PostMessageKeyEnum.VK_F16; return;
                        case '7': found = true; key = User32PostMessageKeyEnum.VK_F17; return;
                        case '8': found = true; key = User32PostMessageKeyEnum.VK_F18; return;
                        case '9': found = true; key = User32PostMessageKeyEnum.VK_F19; return;
                        default:
                            break;
                    }
                }
                if (c1 == '2')
                {
                    switch (keyAlias[2])
                    {
                        case '0': found = true; key = User32PostMessageKeyEnum.VK_F20; return;
                        case '1': found = true; key = User32PostMessageKeyEnum.VK_F21; return;
                        case '2': found = true; key = User32PostMessageKeyEnum.VK_F22; return;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        #region Classic string

        #endregion
        #region Runtime added string

        #endregion


    }
}