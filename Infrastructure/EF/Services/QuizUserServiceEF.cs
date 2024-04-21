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
        QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
        {
            QuizId = quizId,
            UserAnswer = answer,
            UserId = userId,
            QuizItemId = quizItemId
        };
        try
        {
            var savedEntity = _context.Add(entity).Entity;
            _context.SaveChanges();
            return _mapper.Map<QuizItemUserAnswer>(_context.UserAnswers
                .Include(x => x.QuizItem)
                .ThenInclude(x => x.IncorrectAnswers)
                .FirstOrDefault(x => x == savedEntity));
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException.Message.StartsWith("The INSERT"))
            {
                throw new QuizNotFoundException("Quiz, quiz item or user not found. Can't save!");
            }
            if (e.InnerException.Message.StartsWith("Violation of"))
            {
                throw new QuizAnswerItemAlreadyExistsException(quizId, quizItemId, userId);
            }
            throw new Exception(e.Message);
        }

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