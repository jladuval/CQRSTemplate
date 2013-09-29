namespace Web.Models.Home
{
    using System.Collections;
    using System.Collections.Generic;

    public class HomeModel
    {
        public ICollection<ShipModel> Ships { get; set; }
    }

    public class ShipModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}