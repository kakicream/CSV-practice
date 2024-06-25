using UnityEngine;
using System.IO;

public class PlayerRaycast : MonoBehaviour
{
    private string csvFilePath = "Assets/RaycastCollisionData/RaycastCollisionData.csv"; // CSV 파일 저장 경로 설정
    private StreamWriter csvWriter;   
    
    public float raycastDistance = 1000f;  // Raycast의 거리
    private Color rayColor = Color.red;  // Ray의 색상 (빨간색)

    void Start()
    {
        // StreamWriter 초기화
        InitializeStreamWriter();
    }

    void Update()
    {
        // 플레이어가 바라보는 방향 설정
        Vector3 raycastDirection = transform.forward;

        // Raycast 발사
        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastDirection, out hit))
        {
            // Raycast의 교점 위치(hit.point)와 충돌한 오브젝트의 이름(hit.collider.name)을 추출하여 사용
            Vector3 intersectionPoint = hit.point;
            string objectName = hit.collider.name;

            Debug.Log("Object Name: " + objectName + ", Collision Position: " + intersectionPoint);

            // 추가 정보 가져오기
            string colliderTag = hit.collider.tag;

            // CSV 파일에 데이터 추가
            WriteToCSV(objectName, intersectionPoint, colliderTag);
        }
        else
        {
            // 충돌하는 물체가 없을 경우
            WriteToCSV("No collision object found", Vector3.zero, "");
        }
                    Debug.DrawRay(transform.position, raycastDirection * raycastDistance, rayColor);

    }

    void OnDestroy()
    {
        // 스크립트가 파괴될 때 CSV 파일을 닫음
        if (csvWriter != null)
        {
            csvWriter.Close();
        }
    }

    void InitializeStreamWriter()
    {
        try
        {
            // CSV 파일을 쓰기 모드로 열기 (기존 파일이 있으면 덮어씀)
            csvWriter = new StreamWriter(csvFilePath, false);
            csvWriter.WriteLine("Object Name,Collision Position X,Collision Position Y,Collision Position Z,Collider Tag"); // CSV 파일의 헤더
            csvWriter.Flush(); // 버퍼 비우기
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error initializing StreamWriter: " + e.Message);
        }
    }

    void WriteToCSV(string objectName, Vector3 collisionPosition, string colliderTag)
    {
        try
        {
            if (csvWriter != null)
            {
                // CSV 파일에 데이터 추가
                csvWriter.WriteLine(objectName + "," + collisionPosition.x + "," + collisionPosition.y + "," + collisionPosition.z + "," + colliderTag);
                csvWriter.Flush(); // 버퍼 비우기
            }
            else
            {
                Debug.LogWarning("csvWriter is null. StreamWriter might not have been properly initialized.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error writing to CSV: " + e.Message);
        }
    }
}
