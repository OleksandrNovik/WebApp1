namespace BLL.Educational_entities.Education
{
    public class Mark
    {
        public int Id { get; set; }
        public int Value {  get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public ICollection<Work>? Works { get; set; }
        public ICollection<StudentAnswer>? TestAnswers { get; set; }
    }
}
