using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Clock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Transform secArrow, minArrow, hourArrow; 
    private DateTime time;

    private void Start()
    {
        time = DateTime.Now;    
        StartCoroutine(RequestServerTime());
        StartCoroutine(StartTime());
    } 
    private IEnumerator StartTime()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            time = time.AddSeconds(1);
        
            ClockTime clockTime = TimeConverter.ConvertToClockTime(time);
            ShowTime(clockTime);
        }
    }
    private IEnumerator RequestServerTime()
    {
        while (true)
        {
            yield return Server.Send("https://yandex.com/time/sync.json", RefreshServerTime);
            yield return new WaitForSecondsRealtime(Config.Ping);
        }
    } 

    private void RefreshServerTime(UnityWebRequest request)
    {
        if(request.result == UnityWebRequest.Result.Success)
        {
            UnixTime unixTime = new UnixTime();
            unixTime = JsonUtility.FromJson<UnixTime>(request.downloadHandler.text);
            time = TimeConverter.ConvertUnixToDateTime(unixTime.time);
        }
    }
    private void ShowTime(ClockTime clockTime)
    {
        float sec = clockTime.seconds * Config.OneMinSplit;
        float min = clockTime.minutes * Config.OneMinSplit;
        float hour = clockTime.hours * Config.OneSecSplit;

        Vector3 secEueler = new Vector3(0, 0, -sec);
        Vector3 minEueler = new Vector3(0, 0, -min);
        Vector3 hourEueler = new Vector3(0, 0, -hour);

        secArrow.DOLocalRotate(secEueler, Config.TimeDoRotation);
        minArrow.DOLocalRotate(minEueler, Config.TimeDoRotation);
        hourArrow.DOLocalRotate(hourEueler, Config.TimeDoRotation);

        timeText.text = TimeConverter.ConvertTimeToString(time);
    }
}
