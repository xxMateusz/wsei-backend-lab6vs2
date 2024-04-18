namespace Infrastructure.Entities;

public class QuizItemAnswerEntity
{
    public int Id { get; set; }
    public string Answer { get; set; }
    public ISet<QuizItemEntity> QuizItems { get; set; } = new HashSet<QuizItemEntity>();

    public override string ToString()
    {
        return Answer;
    }
}