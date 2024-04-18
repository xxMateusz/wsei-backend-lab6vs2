using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;


[PrimaryKey(nameof(UserId), nameof(QuizId), nameof(QuizItemId))]
public class QuizItemUserAnswerEntity
{
    public int UserId { get; set; }
    public int QuizItemId { get; set; }
    public int QuizId { get; set; }
    public string UserAnswer { get; set; }
    public QuizItemEntity QuizItem { get; set; }
}