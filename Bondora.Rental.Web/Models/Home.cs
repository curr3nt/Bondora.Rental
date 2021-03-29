namespace Bondora.Rental.Web.Models
{
    public class Home : WithSpec
    {
        public ModelSpec Spec { get; }

        public Home(ModelSpec spec)
        {
            Spec = spec;
        }
    }
}
