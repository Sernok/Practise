namespace ConsoleApp1.Domain.Entities
{
    public class Order
    {
        private static int _nextId = 0;
        public int Id { get; }  // идентификатор заказа, доступен только для чтения
        public string OrderNumber { get; set; }  // номер заказа
        public string Place { get; set; }  // место выполнения заказа (в зале или на вынос)
        public DateTime CreationDate { get; set; }  // дата создания заказа
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();  // список позиций заказа
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Order() { }  // конструктор по умолчанию

        public Order(int id)  // конструктор с одним параметром – идентификатором
        {
            Id = id;
        }
        private static int GetNextId()
        {
            return _nextId++;
        }

    }
}
