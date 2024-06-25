using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerPositionExporter : MonoBehaviour
{
    // CSV 파일 저장 경로 설정
    private string filePath = "Assets/PlayerPosition/PlayerPosition.csv";

    void Update()
    {
        // 매 프레임마다 플레이어의 현재 위치를 CSV 파일에 기록
        Vector3 playerPosition = transform.position;
        string[] positionData = { playerPosition.x.ToString(), playerPosition.y.ToString(), playerPosition.z.ToString() };
        string positionString = string.Join(",", positionData);

        // CSV 파일에 위치 정보 추가
        StreamWriter outStream = new StreamWriter(filePath, true);
        outStream.WriteLine(positionString);
        outStream.Close();
    }
}
