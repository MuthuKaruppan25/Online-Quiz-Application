public class QuizMapper
{
    public QuizData MapToQuiz(QuizAddDto quizAddDto)
    {
        var quizData = new QuizData
        {
            Id = GenerateQuizId(),
            Title = quizAddDto.Title,
            Description = quizAddDto.Description,
            durationMinutes = quizAddDto.durationMinutes,
            adminId = quizAddDto.adminId,
            categoryId = quizAddDto.categoryId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
        };

        return quizData;
    }

    private string GenerateQuizId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
