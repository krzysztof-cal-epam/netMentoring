//using CartServiceConsoleApp.DAL.Interfaces;
//using CartServiceConsoleApp.Entities;

//namespace CartServiceConsoleApp.DAL.Databases
//{
//    public class InMemoryCartDatabase : ICartDatabase<Cart>
//    {
//        private readonly Dictionary<Guid, Cart> _store = new();

//        public Cart FindById(Guid id)
//        {
//            _store.TryGetValue(id, out var cart);
//            return cart;
//        }

//        public void Upsert(Cart item)
//        {
//            _store[item.Id] = item;
//        }

//        public void Delete(Guid id)
//        {
//            _store.Remove(id);
//        }

//        public IEnumerable<Cart> GetAll()
//        {
//            return _store.Values.ToList();
//        }
//    }
//}
