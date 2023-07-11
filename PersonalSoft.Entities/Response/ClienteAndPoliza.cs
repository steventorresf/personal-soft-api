namespace PersonalSoft.Entities.Response
{
    public class ClienteAndPoliza
    {
        public Cliente Cliente { get; set; } = new();
        public PlanPoliza PlanPoliza { get; set; } = new();
        public Poliza Poliza { get; set; } = new();
    }
}
