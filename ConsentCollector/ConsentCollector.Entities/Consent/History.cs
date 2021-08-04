namespace ConsentCollector.Entities.Consent
{
    public sealed class History : Entity
    {
        public History(string description) : base()
        {
            this.Description = description;
        }
        public string Description{ get; set; }
    }
}
