namespace ParkingDeluxe;

public interface ITimed
{
    void StartTimedAction();
    void EndTimedAction();
    TimeSpan GetElapsedTime();
}
