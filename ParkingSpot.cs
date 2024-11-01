namespace ParkingDeluxe
{
    public class ParkingSpot
    {
        public int ID {  get; set; }
        public bool IsEmpty { get; set; }
        public Vehicle ? OccupiedBy { get; set; }
        public ParkingSpot(int iD)
        {
            IsEmpty = true;
            ID = iD;
        }
    }
}