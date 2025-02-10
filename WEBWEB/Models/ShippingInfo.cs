using System.ComponentModel.DataAnnotations;

namespace WEBWEB.Models
{
    public class ShippingInfo
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public string Carrier { get; set; }

        public string TrackingNumber { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
