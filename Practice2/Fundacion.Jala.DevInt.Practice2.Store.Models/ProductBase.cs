using System;

namespace Fundacion.Jala.DevInt.Practice2.Store.Models
{
    public abstract class ProductBase
    {
        public ProductBase()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; protected set; }
        public virtual string FullName => throw new NotImplementedException();
    }
}
