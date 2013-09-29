namespace Shipping.Domain
{
    using Base.DDD.Domain;

    public class Ship : Entity
    {
        public string Name { get; set; }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}
