namespace BLL.Educational_entities.Education
{
    public class Mark
    {
        public int Id { get; set; }
        public int Value {  get; set; }
        public (int MinValue, int MaxValue) Range { get; set; }
    }
}
