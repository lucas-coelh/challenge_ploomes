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
        public string Property { get; private set; }

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
                    Property = property
                });
                return false;
            }
            return true;
        }

        public bool ValidPropertyInt(int value, string property)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(property))
            {
                Notifications.Add(new Notifies
                {
                    Message = "Campo Obrigatório",
                    Property = property
                });
                return false;
            }
            return true;
        }

    }
}