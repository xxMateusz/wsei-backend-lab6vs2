namespace Infrastructure.Entities;

public class QuizEntity
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public List<QuizItemEntity> Items { get; set; }
}