using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Notifies
    {
        public Notifies()
        {
            Notifications = new List<Notifies>();
        }

        [NotMapped]
        public string NameProperty { get; private set; }

        [NotMapped]
        public string Message { get; private set; }

        [NotMapped]
        public List<Notifies> Notifications { get; set; }


        public bool ValidPropertyString(string value, string property)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(property))
            {
                Notifications.Add(new Notifies
                {
                    Message = "Campo Obrigatório",
                    NameProperty = property
                });
                return false;
            }
            return true;
        }

    }
}