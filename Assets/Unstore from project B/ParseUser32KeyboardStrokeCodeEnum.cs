using System;

internal class ParseUser32KeyboardStrokeCodeEnum
{
    public static void FindKeyFromAlias(in string keyAlias, out bool found, out User32KeyboardStrokeCodeEnum key)
    {

        #region Char to enum

        found = false;
        key = User32KeyboardStrokeCodeEnum.KEY_A;
        if (keyAlias == null || keyAlias.Length == 0) { found = false; return; }
        if (keyAlias.Length == 1)
        {
            char c = keyAlias[0];
            switch (c)
            {

                case '↓': found = true; key = User32KeyboardStrokeCodeEnum.DOWN; return;
                case '←': found = true; key = User32KeyboardStrokeCodeEnum.LEFT; return;
                case '→': found = true; key = User32KeyboardStrokeCodeEnum.RIGHT; return;
                case '↑': found = true; key = User32KeyboardStrokeCodeEnum.UP; return;
                case ' ': found = true; key = User32KeyboardStrokeCodeEnum.SPACE_BAR; return;
                case '0': found = true; key = User32KeyboardStrokeCodeEnum.KEY_0; return;
                case '1': found = true; key = User32KeyboardStrokeCodeEnum.KEY_1; return;
                case '2': found = true; key = User32KeyboardStrokeCodeEnum.KEY_2; return;
                case '3': found = true; key = User32KeyboardStrokeCodeEnum.KEY_3; return;
                case '4': found = true; key = User32KeyboardStrokeCodeEnum.KEY_4; return;
                case '5': found = true; key = User32KeyboardStrokeCodeEnum.KEY_5; return;
                case '6': found = true; key = User32KeyboardStrokeCodeEnum.KEY_6; return;
                case '7': found = true; key = User32KeyboardStrokeCodeEnum.KEY_7; return;
                case '8': found = true; key = User32KeyboardStrokeCodeEnum.KEY_8; return;
                case '9': found = true; key = User32KeyboardStrokeCodeEnum.KEY_9; return;
                case '-': found = true; key = User32KeyboardStrokeCodeEnum.OEM_MINUS; return;
                case '+': found = true; key = User32KeyboardStrokeCodeEnum.ADD; return;
                case '/': found = true; key = User32KeyboardStrokeCodeEnum.DIVIDE; return;
                case '*': found = true; key = User32KeyboardStrokeCodeEnum.MULTIPLY; return;
                case '.': found = true; key = User32KeyboardStrokeCodeEnum.OEM_COMMA; return;








                         case 'a': case 'A': found = true; key = User32KeyboardStrokeCodeEnum.KEY_A; return;
                         case 'b': case 'B': found = true; key = User32KeyboardStrokeCodeEnum.KEY_B; return;
                         case 'c': case 'C': found = true; key = User32KeyboardStrokeCodeEnum.KEY_C; return;
                         case 'd': case 'D': found = true; key = User32KeyboardStrokeCodeEnum.KEY_D; return;
                         case 'e': case 'E': found = true; key = User32KeyboardStrokeCodeEnum.KEY_E; return;
                         case 'f': case 'F': found = true; key = User32KeyboardStrokeCodeEnum.KEY_F; return;
                         case 'g': case 'G': found = true; key = User32KeyboardStrokeCodeEnum.KEY_G; return;
                         case 'h': case 'H': found = true; key = User32KeyboardStrokeCodeEnum.KEY_H; return;
                         case 'i': case 'I': found = true; key = User32KeyboardStrokeCodeEnum.KEY_I; return;
                         case 'j': case 'J': found = true; key = User32KeyboardStrokeCodeEnum.KEY_J; return;
                         case 'k': case 'K': found = true; key = User32KeyboardStrokeCodeEnum.KEY_K; return;
                         case 'l': case 'L': found = true; key = User32KeyboardStrokeCodeEnum.KEY_L; return;
                         case 'm': case 'M': found = true; key = User32KeyboardStrokeCodeEnum.KEY_M; return;
                         case 'n': case 'N': found = true; key = User32KeyboardStrokeCodeEnum.KEY_N; return;
                         case 'o': case 'O': found = true; key = User32KeyboardStrokeCodeEnum.KEY_O; return;
                         case 'p': case 'P': found = true; key = User32KeyboardStrokeCodeEnum.KEY_P; return;
                         case 'q': case 'Q': found = true; key = User32KeyboardStrokeCodeEnum.KEY_Q; return;
                         case 'r': case 'R': found = true; key = User32KeyboardStrokeCodeEnum.KEY_R; return;
                         case 's': case 'S': found = true; key = User32KeyboardStrokeCodeEnum.KEY_S; return;
                         case 't': case 'T': found = true; key = User32KeyboardStrokeCodeEnum.KEY_T; return;
                         case 'u': case 'U': found = true; key = User32KeyboardStrokeCodeEnum.KEY_U; return;
                         case 'v': case 'V': found = true; key = User32KeyboardStrokeCodeEnum.KEY_V; return;
                         case 'w': case 'W': found = true; key = User32KeyboardStrokeCodeEnum.KEY_W; return;
                         case 'x': case 'X': found = true; key = User32KeyboardStrokeCodeEnum.KEY_X; return;
                         case 'y': case 'Y': found = true; key = User32KeyboardStrokeCodeEnum.KEY_Y; return;
                         case 'z': case 'Z': found = true; key = User32KeyboardStrokeCodeEnum.KEY_Z; return;





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
                    case '1': found = true; key = User32KeyboardStrokeCodeEnum.F1; return;
                    case '2': found = true; key = User32KeyboardStrokeCodeEnum.F2; return;
                    case '3': found = true; key = User32KeyboardStrokeCodeEnum.F3; return;
                    case '4': found = true; key = User32KeyboardStrokeCodeEnum.F4; return;
                    case '5': found = true; key = User32KeyboardStrokeCodeEnum.F5; return;
                    case '6': found = true; key = User32KeyboardStrokeCodeEnum.F6; return;
                    case '7': found = true; key = User32KeyboardStrokeCodeEnum.F7; return;
                    case '8': found = true; key = User32KeyboardStrokeCodeEnum.F8; return;
                    case '9': found = true; key = User32KeyboardStrokeCodeEnum.F9; return;
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
                    case '0': found = true; key = User32KeyboardStrokeCodeEnum.NUMPAD0; return;
                    case '1': found = true; key = User32KeyboardStrokeCodeEnum.NUMPAD1; return;
                    case '2': found = true; key = User32KeyboardStrokeCodeEnum.NUMPAD2; return;
                    case '3': found = true; key = User32KeyboardStrokeCodeEnum.NUMPAD3; return;
                    case '4': found = true; key = User32KeyboardStrokeCodeEnum.NUMPAD4; return;
                    case '5': found = true; key = User32KeyboardStrokeCodeEnum.NUMPAD5; return;
                    case '6': found = true; key = User32KeyboardStrokeCodeEnum.NUMPAD6; return;
                    case '7': found = true; key = User32KeyboardStrokeCodeEnum.NUMPAD7; return;
                    case '8': found = true; key = User32KeyboardStrokeCodeEnum.NUMPAD8; return;
                    case '9': found = true; key = User32KeyboardStrokeCodeEnum.NUMPAD9; return;
                    case '-': found = true; key = User32KeyboardStrokeCodeEnum.OEM_MINUS; return;
                    case '+': found = true; key = User32KeyboardStrokeCodeEnum.ADD; return;
                    case '/': found = true; key = User32KeyboardStrokeCodeEnum.DIVIDE; return;
                    case '*': found = true; key = User32KeyboardStrokeCodeEnum.MULTIPLY; return;
                    case '.': found = true; key = User32KeyboardStrokeCodeEnum.OEM_PERIOD; return;
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
                        case '0': found = true; key = User32KeyboardStrokeCodeEnum.F10; return;
                        case '1': found = true; key = User32KeyboardStrokeCodeEnum.F11; return;
                        case '2': found = true; key = User32KeyboardStrokeCodeEnum.F12; return;
                        case '3': found = true; key = User32KeyboardStrokeCodeEnum.F13; return;
                        case '4': found = true; key = User32KeyboardStrokeCodeEnum.F14; return;
                        case '5': found = true; key = User32KeyboardStrokeCodeEnum.F15; return;
                        case '6': found = true; key = User32KeyboardStrokeCodeEnum.F16; return;
                        case '7': found = true; key = User32KeyboardStrokeCodeEnum.F17; return;
                        case '8': found = true; key = User32KeyboardStrokeCodeEnum.F18; return;
                        case '9': found = true; key = User32KeyboardStrokeCodeEnum.F19; return;
                        default:
                            break;
                    }
                }
                if (c1 == '2')
                {
                    switch (keyAlias[2])
                    {
                        case '0': found = true; key = User32KeyboardStrokeCodeEnum.F20; return;
                        case '1': found = true; key = User32KeyboardStrokeCodeEnum.F21; return;
                        case '2': found = true; key = User32KeyboardStrokeCodeEnum.F22; return;
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