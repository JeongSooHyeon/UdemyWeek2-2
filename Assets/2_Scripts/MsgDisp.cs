using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgDisp : MonoBehaviour
{
    public static string msg;
    public static bool flagDisplay = false;
    public GUIStyle guiDisplay;
    public static int msgLen;
    public static float waitDelay;

    private float nextTime = 0;
    private Rect rtDisplay = new Rect();

    private void OnGUI()
    {
        // UGI 크기 : 16:9의 1280 x 720 해상도 기준
        const float guiScreen = 1280;
        const float guiWidth = 800; // 800x200 픽셀의 메세지 창
        const float guiHeight = 200;
        const float guiLeft = (guiScreen - guiWidth) / 2;   // 가운데
        const float guiTop = 720 - guiHeight - 20;  // 하단

        // 현재 화면과의 비율
        float gui_scale = Screen.width / guiScreen;

        if (flagDisplay)
        {
            // 글꼴 스타일
            GUIStyle msgFont = new GUIStyle
            {
                fontSize = (int)(30 * gui_scale)
            };

            // 메세지창 위치 계산
            rtDisplay.x = guiLeft * gui_scale;
            rtDisplay.y = guiTop * gui_scale;
            rtDisplay.width = guiWidth * gui_scale;
            rtDisplay.height = guiHeight * gui_scale;

            // 메세지창 출력
            GUI.Box(rtDisplay, "창", guiDisplay);

            // 메세지 그림자 출력
            msgFont.normal.textColor = Color.black;
            rtDisplay.x = (guiLeft + 22) * gui_scale;
            rtDisplay.y = (guiTop + 22) * gui_scale;
            GUI.Label(rtDisplay, msg.Substring(0,msgLen), msgFont);


            // 메세지 출력
            msgFont.normal.textColor = Color.white;
            rtDisplay.x = (guiLeft + 20) * gui_scale;
            rtDisplay.y = (guiTop + 20) * gui_scale;
            GUI.Label(rtDisplay, msg.Substring(0, msgLen), msgFont);
        }
    }

    public static void ShowMessage(string msg)
    {
        MsgDisp.msg = msg;
        flagDisplay = true;
        msgLen = 0;
        waitDelay = 0;
    }

    private void Update()
    {
        if (flagDisplay)
        {
            if (msgLen < msg.Length)
            {
                if (Time.time > nextTime)
                {
                    msgLen++;
                    nextTime = Time.time + 0.02f;
                }
            }
            else // 메세지 모두 출력 후 
            {
                waitDelay += Time.deltaTime;    // 대기 시간 계산
                if (waitDelay > 1 + msg.Length / 4)
                    flagDisplay = false;
            }
        }
    }
}
