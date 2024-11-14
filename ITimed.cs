namespace ParkingDeluxe;

public interface ITimed
{
    void StartTimer();
    void EndTimer();
    TimeSpan GetElapsedTime();
}
