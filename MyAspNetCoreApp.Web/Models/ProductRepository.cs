using System.Diagnostics.Contracts;

namespace MyAspNetCoreApp.Web.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>() //İlk yüklendiğinde dataları yüklemesi için static olan property ilk initialize edildiğin anda bu dataları yükleyecektir.
        {
            //new () { Id = 1, Name = "Kalem 1", Price = 100, Stock = 100 }, 
            //new () { Id = 2, Name = "Kalem 2", Price = 200, Stock = 200 },
            //new () { Id = 3, Name = "Kalem 3", Price = 300, Stock = 300 }
        }; //Nesne örneği oluşturuldu.Aşağıda yapılıcak özelliklerle erişebilir hale getirilicektir.
        public List<Product> GetAll() => _products; //Birşey dönülmeyecekse bu şekilde yazılırsa return olarak dönmeye gerek yoktur.
        public void Add(Product newProduct) => _products.Add(newProduct);
        public void Remove(int Id) //Bir Id tanımlamadığımız için yukarıda şekilde gösterilemiyor.Süslü parantez içerisine tanımlama yapılacaktır.
        {
            var hasProduct = _products.FirstOrDefault(p => p.Id == Id);
            if (hasProduct == null)
            {
                throw new Exception($"Bu id({Id})'ye sahip ürün bulunmamaktadır."); //Dışarıdan ifade tanımladığımızda başına $ işareti koyulur.
            }
            _products.Remove(hasProduct);
        }
        public void Update(Product updateProduct)
        {
            var hasProduct = _products.FirstOrDefault(p => p.Id == updateProduct.Id);
            if (hasProduct == null)
            {
                throw new Exception($"Bu id({updateProduct.Id})'ye sahip ürün bulunmamaktadır.");
            }
            hasProduct.Name= updateProduct.Name;
            hasProduct.Price= updateProduct.Price;
            hasProduct.Stock= updateProduct.Stock; //Bu şekilde yazarak güncellendi fakat güncellenen bilgiler tekrar yazılmalıdır.
            var index = _products.FindIndex(x => x.Id == updateProduct.Id);
            _products[index] = hasProduct; //Yeni ürün hasProduct'a güncellendi.

        }
    }
}
