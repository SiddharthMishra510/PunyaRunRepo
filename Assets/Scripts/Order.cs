public class Order
{
    public int Id;
    public House House;

    public Order(int orderId, House house)
    {
        Id = orderId;
        House = house;
    }
}