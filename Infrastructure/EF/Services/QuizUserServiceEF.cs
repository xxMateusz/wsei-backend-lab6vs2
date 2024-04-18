using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Services;

public class QuizUserServiceEF : IQuizUserService
{
    private readonly QuizDbContext _context;
    private readonly IMapper _mapper;

    public QuizUserServiceEF(QuizDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public Quiz CreateAndGetQuizRandom(int count)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Quiz> FindAllQuizzes()
    {
        return _context
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .Select(element => _mapper.Map<Quiz>(element))
            .ToList();
    }

    public Quiz? FindQuizById(int id)
    {
        var entity = _context
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .FirstOrDefault(e => e.Id == id);
        return entity is null ? null : _mapper.Map<Quiz>(entity);
    }

    public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
    {
        var quizzEntity = _context.Quizzes.FirstOrDefault(q => q.Id == quizId);
        // if (quizzEntity is null)
        // {
        //     throw new QuizNotFoundException($"Quiz with id {quizId} not found");
        // }

        var item = _context.QuizItems.FirstOrDefault(qi => qi.Id == quizItemId);
        // if (item is null)
        // {
        //     throw new QuizItemNotFoundException($"Quiz item with id {quizId} not found");
        // }
        QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
        {
            QuizId = quizId,
            UserAnswer = answer,
            UserId = userId,
            QuizItemId = quizItemId
        };
        var savedEntity = _context.Add(entity).Entity;
        _context.SaveChanges();
        return _mapper.Map<QuizItemUserAnswer>(_context.UserAnswers
            .Include(x => x.QuizItem)
            .ThenInclude(x => x.IncorrectAnswers)
            .FirstOrDefault(x => x == savedEntity));
    }

    public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
    {
        var userAnswers = _context.UserAnswers
            .Include(x => x.QuizItem)
            .ThenInclude(x => x.IncorrectAnswers)
            .Where(a => a.UserId == userId && a.QuizId == quizId)
            .ToList();
        return _mapper.Map<List<QuizItemUserAnswer>>(userAnswers);
    }
}