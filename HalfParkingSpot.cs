namespace ParkingDeluxe;

public class HalfParkingSpot
{
    public int ID { get; }
    public bool IsEmpty { get; set; }
    public Vehicle ? OccupyingVechicle { get; set; }

    public HalfParkingSpot(int iD)
    {
        IsEmpty = true;
        ID = iD;
    }
}
